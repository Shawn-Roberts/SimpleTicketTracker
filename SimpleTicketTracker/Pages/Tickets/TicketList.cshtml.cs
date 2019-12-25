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
    public class TicketListModel : PageModel
    {
        //FIELDS
        private readonly ITicketRepository _ticketData;
        private readonly ILogger<Ticket> _logger;
        public IEnumerable<Ticket> Tickets;

        [TempData]
        public string StatusMessage { get; set; }

        //FILTERS
        public IEnumerable<SelectListItem> Status;
        public IEnumerable<SelectListItem> Projects;
        public IEnumerable<SelectListItem> Priority;
        public IEnumerable<SelectListItem> Owner;
        public IEnumerable<SelectListItem> RequestType;

        //CONSTRUCTORS
        public TicketListModel(ITicketRepository ticketdata, ILogger<Ticket> logger)
        {
            this._ticketData = ticketdata;
            this._logger = logger;
        }


        //METHODS
        public async Task<IActionResult> OnGet(string? priority)
        {
            Status = await _ticketData.GetTicketStatusDropdownAsync();
            Projects = await _ticketData.GetProjectsDropdownAsync();
            Priority = await _ticketData.GetPriorityDropdownAsync();
            Owner = await _ticketData.GetUsersDropdownAsync();
            RequestType = await _ticketData.GetRequestTypeDropdownAsync();

            _logger.LogInformation("Getting tickets for display");
            if (!String.IsNullOrEmpty(priority))
            {
                switch (priority.ToLower())
                {
                    case "critical":
                        {
                            Tickets = await _ticketData.GetTicketByImportanceAsync(3);
                            break;
                        }
                    case "high":
                        {
                            Tickets = await _ticketData.GetTicketByImportanceAsync(2);
                            break;
                        }
                    case "regular":
                        {
                            Tickets = await _ticketData.GetTicketByImportanceAsync(1);
                            break;
                        }
                    default:
                        Tickets = await _ticketData.GetAllTicketsAsync();
                        break;
                }
            }
            else Tickets = await _ticketData.GetAllTicketsAsync();
            return Page();
        }


    }
}