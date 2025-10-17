using tests.DTOs;
using tests.Models;

namespace tests.Services.IServices
{
    public interface IJwtService
    {
        public string CreateToken(UserModel userCreateDTO, List<RoleModel> roles);
        public string GenerateRefreshToken();
    }
}
