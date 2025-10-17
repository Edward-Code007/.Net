using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tests.Models
{
    public class UserModel
    {
        public int Id { get; set; } = default(int);
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        public string? RefreshToken {  get; set; } = string.Empty;
        public DateTime? ExpireTimeToken {  get; set; } = null;
        [JsonIgnore]
        public bool isActive { get; set; } = true;
        public List<RoleModel> Role { get; set; } = [];
        [JsonIgnore]
        public byte[] Salt { get; set; } = [];

    }
}
