using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Models
{
    public class SignInModel
    {
        [Required]
        public string Username { get; set; }
        
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }
    }
}
