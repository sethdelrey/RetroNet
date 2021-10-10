using _90sTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Models
{
    public class AdminModel
    {
        public List<ReportedPost> ReportedPosts { get; set; }
        public List<ReportedUser> ReportedUsers { get; set; }
    }
}
