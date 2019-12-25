using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SimpleTicketTracker.Core.Models;
using SimpleTicketTracker.Data;

namespace SimpleTicketTracker
{
    public class TicketDetailsModel : PageModel
    {
        //FIELDS
        private readonly ITicketRepository _ticketData;
        private readonly ILogger<Ticket> _logger;

        [BindProperty]
        public Ticket Ticket { get; set; }

        public string Project { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IEnumerable<TicketComment> TicketComments { get; set; }

        public string ProjectName { get; set; }
        public string TicketOwner { get; set; }
        public string TicketPriority { get; set; }
        public string RequestType { get; set; }
        public string TicketStatus { get; set; }

        //CONSTRUCTORS
        public TicketDetailsModel(ITicketRepository ticketdata, ILogger<Ticket> logger)
        {
            this._ticketData = ticketdata;
            this._logger = logger;
        }

        //METHODS

        public async Task<IActionResult> OnGet(int ticketId)
        {
            Ticket = await _ticketData.GetTicketByIdAsync(ticketId);
            TicketComments = await _ticketData.GetTicketCommentsByIdAsync(ticketId);
            ProjectName = _ticketData.GetProjectName(Ticket.ProjectId);
            TicketOwner = _ticketData.GetTickerOwnerName(Ticket.OwnerId);
            TicketPriority = _ticketData.GetTicketPriorityName(Ticket.PriorityId);
            RequestType = _ticketData.GetTicketRequestTypeName(Ticket.TypeId);
            TicketStatus = _ticketData.GetRequestStatusName(Ticket.StatusId);


            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var ticketcomment = Request.Form["TicketComment"];
            if (String.IsNullOrEmpty(ticketcomment))
            {
                _logger.LogInformation("No comment added");
                StatusMessage = String.Format("No comment added");
                return RedirectToPage("Index");
            }
            if (await _ticketData.AddTicketCommentAsync(ticketcomment, Ticket.TicketId) > 0)
            {
                _logger.LogInformation("Comment sucessfully added");
                StatusMessage = String.Format("Comment successfully added");
                return RedirectToPage("Index");

            }
            _logger.LogInformation("Error adding comment");
            StatusMessage = String.Format("Error adding comment");
            return RedirectToPage("Index");
        }
    }
}
