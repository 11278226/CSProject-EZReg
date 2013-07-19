using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZ_Regulatory3.Models
{
    public class QuestionChecklistLink
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
    }
}