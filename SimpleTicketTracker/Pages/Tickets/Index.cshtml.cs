using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimpleTicketTracker.Core.Models;
using SimpleTicketTracker.Data;

namespace SimpleTicketTracker
{
    public class IndexModel : PageModel
    {
        //FIELDS
        private readonly ITicketRepository _ticketdata;
        private readonly ILogger<Ticket> _logger;

        public IEnumerable<Ticket> critcalTickets;
        public IEnumerable<Ticket> highTickets;
        public IEnumerable<Ticket> regularTickets;
        public int AllCritcalTickets;
        public int AllHighTickets;
        public int AllRegularTickets;

        [TempData]
        public string StatusMessage { get; set; }


        //CONSTRUCTORS
        public IndexModel(ITicketRepository context, ILogger<Ticket> logger)
        {
            this._ticketdata = context;
            this._logger = logger;
        }

        //METHODS
        public async Task<IActionResult> OnGet()
        {
            _logger.LogInformation("Getting ticket by importance");
            critcalTickets = await _ticketdata.GetTicketByImportanceAsync(3, 5);
            highTickets = await _ticketdata.GetTicketByImportanceAsync(2, 5);
            regularTickets = await _ticketdata.GetTicketByImportanceAsync(1, 5);
            AllCritcalTickets = _ticketdata.GetTicketCount(3);
            AllHighTickets = _ticketdata.GetTicketCount(2);
            AllRegularTickets = _ticketdata.GetTicketCount(1);
            return Page();
        }
    }
}