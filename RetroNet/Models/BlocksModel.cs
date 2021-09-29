using _90sTest.Areas.Identity.Data;
using _90sTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Models
{
    public class BlocksModel
    {
        public List<RetroNetUser> BlockedByList { get; set; }

        public List<RetroNetUser> BlockedList { get; set; }

        public RetroNetUser User { get; set; }

        public bool IsBlocked { get; set; }

        public int BlockeByCount { get; set; }

        public int BlockedCount { get; set; }
    }
}
