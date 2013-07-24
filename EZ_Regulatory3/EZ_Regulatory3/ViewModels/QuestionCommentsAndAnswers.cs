using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZ_Regulatory3.ViewModels
{
    public class QuestionCommentsAndAnswers
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public string QuestionAnswer { get; set; }
        public string QuestionComment { get; set; }
    }
}