using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory3.Models
{
    public class Question
    {
        public int QuestionID { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateModified { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string CompliantAnswer { get; set; }

        public virtual Survey Surveys { get; set; }
    }
}