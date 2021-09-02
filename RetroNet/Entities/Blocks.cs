using _90sTest.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class Blocks
    {
        public string UserId { get; set; }
        public virtual RetroNetUser User { get; set; }

        public string BlockerId { get; set; }
        public virtual RetroNetUser Blocker { get; set; }
    }
}
