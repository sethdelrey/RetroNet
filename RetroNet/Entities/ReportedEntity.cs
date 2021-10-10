using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class ReportedEntity<T>
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public string UserId { get; set; }

        [DisplayName("Desciption")]
        [Required(ErrorMessage = "Please write a short description of your grievances.")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [DisplayName("What is wrong about the post? (Select one)")]
        [Required(ErrorMessage = "Please select one.")]
        public ReportType Type { get; set; }
    }
}
