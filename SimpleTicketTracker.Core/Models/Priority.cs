using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleTicketTracker.Core.Models
{
    public class Priority
    {
        [Key]
        public int PriorityId { get; set; }

        [Required, DataType(DataType.Text), StringLength(maximumLength: 10, MinimumLength = 3), Display(Name = "Priority", Prompt = "Please enter a priority")]
        public string Name { get; set; }

        [Required, DataType(DataType.Text), StringLength(maximumLength: 25, MinimumLength = 3), Display(Name = "Priority Description", Prompt = "Please enter a description")]
        public string Description { get; set; }

    }
}
