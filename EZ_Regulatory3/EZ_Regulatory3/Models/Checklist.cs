using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory3.Models
{
    public class Checklist
    {
        public int ID { get; set; }

        public int SurveyID { get; set; }

        public int UserID { get; set; }

        [Required]
        public string Approved { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateSubmitted { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateApproved { get; set; }
    }
}