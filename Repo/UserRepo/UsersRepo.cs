using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using tests.Data;
using tests.Models;
using tests.Repo.UserRepo.DTO;
using tests.Repo.UserRepo.Features;
using tests.Repo.UserRepo.Mapper;

namespace tests.Repo.UserRepo
{
    public class UsersRepo(IUserFeature userFeature) : IUsersRepo
    {
        private readonly IUserFeature _userFeature = userFeature;
      
        public async Task<MapFromUserModule[]> GetUsers()
        {
            var usuarios = _userFeature.getUsers();
            return await (from user in usuarios
                          select new MapFromUserModule() {
                              Id=user.Id,
                              Name=user.Name,
                              Email=user.Email,
                              Role= (from roles in user.Role
                                    select roles.Name)
                          }
                    ).ToArrayAsync();  
                    

        }

        public async Task<MapFromUserModule> CreateUser(UserCreateDTO user)
        {
            UserModel userCreated = await _userFeature.CreateUser(user);
            return new()
            {
                Id = userCreated.Id,
                Name = userCreated.Name,
                Email=userCreated.Email,
            };
        }

        public async Task<MapFromUserModule?> GetUserById(int id)
        {
            IQueryable<UserModel> user = _userFeature.GetByID(id);
            UserModel? userModel = await user.SingleOrDefaultAsync();
            
            return userModel is not null ? new MapFromUserModule() 
            {
            Id= userModel.Id,
            Name = userModel.Name,
            Email=userModel.Email,
            Role= [.. (from role in userModel.Role
                  select role.Name)]
            } : null;
            
        }
       
        public async Task<MapFromUserModule?> DeleteUser(int id)
        {
          UserModel? userDeleted =  await _userFeature.deleteById(id);
            return userDeleted is null ?
                null : new MapFromUserModule() { 
                Id= userDeleted.Id,
                Name= userDeleted.Name,
                Email= userDeleted.Email,
            };

        }

        public MapFromUserModule UpdateUser(UpdateUserDTO user,int id)
        {
            throw new NotImplementedException();
        }

        public string? LastSeen(int id)
        {
           UserModel? user = this._userFeature.getContext().User.Single(x => x.Id == id);
            if (user is null) return null;

            return user.ExpireTimeToken?.ToString("d");
        }
    }
}
