using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tests.Data;
using tests.Services;
using tests.Services.IServices;
using Serilog;
using FluentValidation;
using tests.Repo.UserRepo.DTO;
using ILogger = Serilog.ILogger;
using tests.Config;

namespace tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args,
            });
            
           
            var connectionString = builder.Configuration.GetConnectionString("default")
                ?? throw new ArgumentNullException("Error al cargar  Connection String");
            // Add services to the container.

            builder.Host.UseSerilog((context, configuration) =>
                 configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddControllers()
                .AddNewtonsoftJson();
         
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            JwtOption jwtOption = new();
            var jwtOptionSection = builder.Configuration.GetSection(nameof(JwtOption));
            jwtOptionSection.Bind(jwtOption);
            builder.Services.Configure<JwtOption>(jwtOptionSection);


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOpt =>
                {
                    jwtOpt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOption.Issuer,
                        ValidateIssuer = true,
                        ValidAudience = jwtOption.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey= new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddCustomServices();


            builder.Configuration.AddEnvironmentVariables();
            

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using (var serv = app.Services.CreateScope())
            {
                var DIC = serv.ServiceProvider;
                var loger = DIC.GetService<ILogger>();
                loger!.Information("Servidor Iniciado Con Exito");
            }

                app.Run();
        }
    }
}
