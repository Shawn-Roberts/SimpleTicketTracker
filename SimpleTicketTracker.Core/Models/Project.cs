using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleTicketTracker.Core.Models
{
    public class Project
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Required, StringLength(60, MinimumLength = 5),]
        [Display(Name = "Project Name", Prompt = "Please enter a project name")]
        public string Name { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Text), StringLength(250), Display(Name = "Project Description", Prompt = "Enter a project description")]
        public string Description { get; set; }
    }
}
