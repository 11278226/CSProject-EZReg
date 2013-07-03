using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EZ_Regulatory2.Models
{
    public class MyModelDBContext : DbContext
    {
        public DbSet<MyModel> MyModels { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
    public class MyModel
    {
        public int ID { get; set; }
    }

    public class Question
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateModified { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string CompliantAnswer { get; set; }
    }
    
    public class Survey
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateModified { get; set; }

        [Required]
        public string Submitted { get; set; }

        [Required]
        public string Approved { get; set; }

        [Required]
        public string Month { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string DateAdded { get; set; }

        public virtual List<Question> questions { get; set; }
    }
}