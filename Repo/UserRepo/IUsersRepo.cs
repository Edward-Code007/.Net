using tests.Models;
using tests.Repo.UserRepo.DTO;
using tests.Repo.UserRepo.Mapper;

namespace tests.Repo.UserRepo
{
    public interface IUsersRepo
    {
        public Task<MapFromUserModule[]> GetUsers();
        public Task<MapFromUserModule> CreateUser(UserCreateDTO user);
        public Task<MapFromUserModule?> GetUserById(int id);
        public MapFromUserModule UpdateUser(UpdateUserDTO user,int id);
        public Task<MapFromUserModule?> DeleteUser(int id);

        public string? LastSeen(int id);
  
    }
}
