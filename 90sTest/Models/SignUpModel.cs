using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _90sTest.Models
{
    public class SignUpModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
