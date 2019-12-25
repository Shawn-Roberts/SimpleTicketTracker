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
    public class CreateTicketModel : PageModel
    {

        //FIELDS
        private readonly ITicketRepository _ticketData;
        private readonly ILogger<Ticket> _logger;
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Ticket Ticket { get; set; }
        public IEnumerable<SelectListItem> Projects;
        public IEnumerable<SelectListItem> Priority;
        public IEnumerable<SelectListItem> Owner;
        public IEnumerable<SelectListItem> RequestType;
        public IEnumerable<SelectListItem> Status;

        //CONSTRUCTORS
        public CreateTicketModel(ITicketRepository context, ILogger<Ticket> logger)
        {
            this._ticketData = context;
            this._logger = logger;
        }


        //METHODS
        public async Task<IActionResult> OnGetAsync()
        {
            Projects = await _ticketData.GetProjectsDropdownAsync();
            Priority = await _ticketData.GetPriorityDropdownAsync();
            Owner = await _ticketData.GetUsersDropdownAsync();
            RequestType = await _ticketData.GetRequestTypeDropdownAsync();
            Status = await _ticketData.GetTicketStatusDropdownAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Unable to create ticket due to invalid input");
                Projects = await _ticketData.GetProjectsDropdownAsync();
                Priority = await _ticketData.GetPriorityDropdownAsync();
                Owner = await _ticketData.GetUsersDropdownAsync();
                RequestType = await _ticketData.GetRequestTypeDropdownAsync();
                Status = await _ticketData.GetTicketStatusDropdownAsync();
                return Page();
            }
            if (await _ticketData.Add(Ticket) > 0)
            {
                _logger.LogInformation("Ticket sucessfully added");
                StatusMessage = String.Format("Ticket successfully added");
                return RedirectToPage("Index");

            }
            else
            {
                _logger.LogInformation("Ticket unsucessfully added");
                StatusMessage = String.Format("Unable to add ticket");
                return RedirectToPage("Index");
            }
        }
    }
}