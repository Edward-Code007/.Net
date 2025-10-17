using tests.Models;

namespace tests.Repo.UserRepo.Mapper
{
    public class MapFromUserModule
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<string> Role = [];

    }
}
