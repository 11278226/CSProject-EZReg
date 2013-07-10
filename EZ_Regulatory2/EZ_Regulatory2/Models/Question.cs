using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EZ_Regulatory2.Models;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory2.Models
{
    public class Question
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateModified { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string CompliantAnswer { get; set; }
    }
}