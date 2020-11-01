using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        public User()
        {

        }

        public User(string p_username, string fName, string lName)
        {
            Username = p_username;
            FirstName = fName;
            LastName = lName;
        }
    }
}
