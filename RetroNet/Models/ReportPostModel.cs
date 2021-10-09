using _90sTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Models
{
    public class ReportPostModel
    {
        public Post Post { get; set; } 
        public ReportedPost ReportedPost { get; set; }
    }
}
