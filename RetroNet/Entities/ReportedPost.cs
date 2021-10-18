using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _90sTest.Entities
{
    public class ReportedPost
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        [ForeignKey("Posts")]
        [Required]
        [MaxLength(36)]
        public string PostId { get; set; }

        [ForeignKey("AspNetUsers")]
        [Required]
        [MaxLength(36)]
        public string ReportingUserId { get; set; }

        [DisplayName("Desciption")]
        [Required(ErrorMessage = "Please write a short description of your grievances.")]
        [MaxLength(1000, ErrorMessage = "The description has to be less than 1000 characters.")]
        public string Description { get; set; }

        [DisplayName("What is wrong about the post? (Select one)")]
        [Required(ErrorMessage = "Please select one.")]
        public ReportType Type { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReportTime { get; set; }
    }
}
