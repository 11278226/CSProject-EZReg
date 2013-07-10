using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EZ_Regulatory2.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EZ_Regulatory2.DAL
{
    public class SurveyContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
}