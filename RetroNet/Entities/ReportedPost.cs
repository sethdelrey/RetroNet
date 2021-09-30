using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class ReportedPost
    {
        public Guid Id { get; set; }

        public int PostId { get; set; }

        public string Description { get; set; }

        public ReportType Type { get; set; }

        public enum ReportType
        {
            Bot,
            Porn,
            Offensive,
            Spam,
            Dox,
            Violence
        }
    }
}
