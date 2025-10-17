using System.ComponentModel.DataAnnotations;

namespace tests.DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email {  get; set; }
        [StringLength(50), MinLength(8)]
        public string Password { get; set; }
    }
}
