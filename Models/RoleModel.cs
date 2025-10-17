using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tests.Models
{
   
    public class RoleModel
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; } = string.Empty;

        [JsonIgnore]
        public List<UserModel> User { get; set; } = null!;
    }
}
