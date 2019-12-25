using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleTicketTracker.Core.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Required, DataType(DataType.Text), StringLength(maximumLength: 10, MinimumLength = 3), Display(Name = "Status", Prompt = "Please enter a status")]
        public string Name { get; set; }

        [Required, DataType(DataType.Text), StringLength(maximumLength: 25, MinimumLength = 3), Display(Name = "Status Description", Prompt = "Please enter a status")]
        public string Description { get; set; }

    }
}
