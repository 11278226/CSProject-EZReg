using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory3.Models
{
    public class Survey
    {
        public Survey() { Submitted = "No"; Approved = "No"; DateAdded = System.DateTime.Now; }
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateEnd { get; set; }

        [Required]
        public string Submitted { get; set; }

        [Required]
        public string Approved { get; set; }

        [Required]
        public string Month { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateAdded { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }

    }
}