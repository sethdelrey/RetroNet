using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _90sTest.Entities
{
    public class ReportedPost
    {
        public Guid Id { get; set; }

        public int PostId { get; set; }

        [DisplayName("Desciption")]
        [Required(ErrorMessage = "Please write a short description of your grievances.")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [DisplayName("What is wrong about the post? (Select one)")]
        [Required(ErrorMessage = "Please select one.")]
        public ReportType Type { get; set; }

        public enum ReportType
        {
            Bot,
            Offensive,
            Spam,
            Dox,
            Violence
        }
    }
}
