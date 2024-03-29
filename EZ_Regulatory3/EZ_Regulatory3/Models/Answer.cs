﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory3.Models
{
    public class Answer
    {
        public int ID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        public string QuestionAnswer { get; set; }

        public string QuestionComment { get; set; }

    }
}