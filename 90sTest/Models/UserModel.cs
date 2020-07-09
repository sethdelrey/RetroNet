using System.ComponentModel.DataAnnotations;

namespace _90sTest.Models
{
    public class UserModel
    {
        [StringLength(40, MinimumLength = 1)]
        [Required(ErrorMessage = "Please enter a username that that is smaller than 40 characters.")]
        [Key]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = "Please enter your first name. (Must be smaller than 50 characters)")]
        public string FirstName { get; set; }

        [StringLength(70, MinimumLength = 1)]
        [Required(ErrorMessage = "Please enter your last name. (Must be smaller than 70 characters)")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
