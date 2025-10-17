using tests.Models;
using tests.Repo.RoleRepo.Mappers;

namespace tests.Repo.RoleRepo
{
    public interface IRoleRepo<T> where T : RoleModel
    {
        public IQueryable<T> getRoles();
    }
}
