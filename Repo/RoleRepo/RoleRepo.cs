using tests.Data;
using tests.Models;
using tests.Repo.RoleRepo.Mappers;

namespace tests.Repo.RoleRepo
{
    public class RoleRepo : IRoleRepo<RoleModel>
    {
        public AppDbContext _context;
        public RoleRepo(AppDbContext context) { 
            this._context = context;
        }
        public IQueryable<RoleModel> getRoles()
        {
            IQueryable<RoleModel> roles = from role in this._context.Role
                                          select role;
            return roles;
        }
    }
}
