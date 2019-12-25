using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleTicketTracker.Core.Models
{
    public class TicketType
    {
        [Key]
        public int TypeId { get; set; }

        [Required, DataType(DataType.Text), StringLength(maximumLength: 10, MinimumLength = 3), Display(Name = "Ticket Type", Prompt = "Please enter a ticket type")]
        public string Type { get; set; }

        [Required, DataType(DataType.Text), StringLength(maximumLength: 25, MinimumLength = 3), Display(Name = "Type Description", Prompt = "Please enter a type description")]
        public string Description { get; set; }
    }
}
