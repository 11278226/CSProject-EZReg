using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory3.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserType { get; set; }

        public string EmailAddress { get; set; }

        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }


    }
}