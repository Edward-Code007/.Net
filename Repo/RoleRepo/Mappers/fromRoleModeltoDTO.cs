using System.ComponentModel.DataAnnotations;
using tests.Models;

namespace tests.Repo.RoleRepo.Mappers
{
    public  class fromRoleModeltoDTO
    {
        public int Id { get; set; }
        public String Name { get; set; } = string.Empty;
    }
}
