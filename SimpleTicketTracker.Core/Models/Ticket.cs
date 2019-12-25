using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleTicketTracker.Core.Models
{
    public class Ticket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        [ForeignKey("Project"), Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage ="Ticket name is required"), StringLength(60, MinimumLength = 5),]
        [Display(Name = "Ticket Name", Prompt = "Please enter a ticket name")]
        public string Name { get; set; }

        [StringLength(250,ErrorMessage ="Ticket description cannot be more than 250 characters"), Display(Name = "Ticket Description")]
        public string Description { get; set; }

        [ForeignKey("Priority"), Required (ErrorMessage ="Ticket priority is required"), Display(Name = "Ticket Priority")]
        public int PriorityId { get; set; }

        [ForeignKey("ApplicationUser"), Required(ErrorMessage = "Ticket owner is required"), Display(Name = "Owner", Prompt = "Please select a ticket owner")]
        public int OwnerId { get; set; }

        [ForeignKey("TicketType"), Required(ErrorMessage = "Ticket type is required"), Display(Name = "Request Type", Prompt = "Please select a request type")]

        public int TypeId { get; set; }

        [Required(ErrorMessage = "Ticket status is required"), ForeignKey("Status"), Display(Name = "Ticket Status")]
        public int StatusId { get; set; }

        [Display(Name = "Total Hours Worked")]
        public int HoursWorked { get; set; } = 0;

        [Required(ErrorMessage = "Creation Date is required"), DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Due date is required"), DataType(DataType.Date), Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

    }
}
