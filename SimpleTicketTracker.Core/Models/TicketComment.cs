using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleTicketTracker.Core.Models
{
    public class TicketComment
    {
        [Key]
        public int CommentId { get; set; }

        [ForeignKey("Ticket")]
        public int TicketID { get; set; }

        [Required]
        public int CommentorId { get; set; }

        [Required]
        public string Comment { get; set; }


        [Required, DataType(DataType.DateTime)]
        public DateTime CommentDate { get; set; }
    }
}
