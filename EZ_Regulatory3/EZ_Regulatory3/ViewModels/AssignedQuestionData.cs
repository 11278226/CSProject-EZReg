using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZ_Regulatory3.ViewModels
{
    public class AssignedQuestionData
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}