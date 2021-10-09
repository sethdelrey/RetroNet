using _90sTest.Areas.Identity.Data;
using _90sTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Models
{
    public class ReportUserModel
    {
        public RetroNetUser User { get; set; }
        public ReportedUser ReportedUser { get; set; }
    }
}
