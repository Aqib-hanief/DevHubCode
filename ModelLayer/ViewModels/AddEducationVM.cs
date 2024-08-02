using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class AddEducationVM
    {
        [Required]
        public string School { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string FieldOfStudy { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }
        public bool IsCurrent { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
