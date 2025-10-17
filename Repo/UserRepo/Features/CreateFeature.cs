using Microsoft.EntityFrameworkCore;
using tests.Data;
using tests.Models;
using tests.Repo.UserRepo.DTO;
using tests.Services.IServices;

namespace tests.Repo.UserRepo.Features
{
    public static class CreateFeature
    {
        public static async Task<UserModel> CreateUser(
            this IUserFeature userFeature,
            UserCreateDTO create)
        {
            AppDbContext context = userFeature.getContext();
          ICrypto crypto = userFeature.GetCrypto();
            byte[] salt = [];

           string hashedPass = crypto.Encrypt(create.Password, salt);

            UserModel userNew = new()
            {
                Name=create.Name,
                Email=create.Email,
                Password=hashedPass,
                Salt=salt,
                Role= [ await context.Role.SingleAsync(x => x.Id == 1)]
            };

           await context.User.AddAsync(userNew);
           await context.SaveChangesAsync();
           return userNew;

        }
    }
}
