using FluentValidation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using tests.Repo.UserRepo;
using tests.Repo.UserRepo.DTO;
using tests.Repo.UserRepo.Features;
using tests.Services;
using tests.Services.IServices;

namespace tests
{
    public static class CustomServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection collection)
        {
            collection.AddScoped<IValidator<UserCreateDTO>, UserCreateValidator>();
            collection.AddScoped<IJwtService, JwtService>();
            collection.AddScoped<ICrypto, Crypto>();
            collection.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"Data"));
            collection.AddScoped<IUsersRepo, UsersRepo>();
            collection.AddScoped<IUserFeature, UserFeature>();
            return collection;

        }

    }
}
