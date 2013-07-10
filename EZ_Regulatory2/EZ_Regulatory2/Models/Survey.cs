using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EZ_Regulatory2.Models;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory2.Models
{
    public class Survey
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateModified { get; set; }

        [Required]
        public string Submitted { get; set; }

        [Required]
        public string Approved { get; set; }

        [Required]
        public string Month { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string DateAdded { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}