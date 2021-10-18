﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class ReportedUser
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        [ForeignKey("AspNetUsers")]
        [Required]
        [MaxLength(36)]
        public string UserId { get; set; }

        [ForeignKey("AspNetUsers")]
        [Required]
        [MaxLength(36)]
        public string ReportingUserId { get; set; }

        [DisplayName("Desciption")]
        [Required(ErrorMessage = "Please write a short description of your grievances.")]
        [MaxLength(1000, ErrorMessage = "The description has to be less than 1000 characters.")]
        public string Description { get; set; }

        [DisplayName("What is wrong about the user? (Select one)")]
        [Required(ErrorMessage = "Please select one.")]
        public ReportType Type { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReportTime { get; set; }
    }
}
