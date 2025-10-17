using System.ComponentModel.DataAnnotations;
using tests.Models;

namespace tests.Repo.UserRepo.DTO
{
    public class UserCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public byte[] salt { get; set; } = [];
    }
}
