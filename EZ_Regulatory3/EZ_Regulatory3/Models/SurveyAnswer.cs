using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory3.Models
{
    public class SurveyAnswer
    {

        public int ID { get; set; }

        [Required]
        public int SurveyID { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}