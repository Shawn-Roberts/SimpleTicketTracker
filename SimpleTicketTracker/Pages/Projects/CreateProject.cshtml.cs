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
    public class CreateProjectModel : PageModel
    {

        //FIELDS
        private readonly ITicketRepository _projectData;
        private readonly ILogger<Project> _logger;

        [BindProperty]
        public Project Project { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        //CONSTRUCTORS
        public CreateProjectModel(ITicketRepository context, ILogger<Project> logger)
        {
            this._projectData = context;
            this._logger = logger;
        }

        //METHODS
        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (await _projectData.Add(Project) > 0)
            {
                _logger.LogInformation("Project sucessfully added");
                StatusMessage = String.Format("Project successfully added");
                return RedirectToPage("./ManageProjects");

            }
            else
            {
                _logger.LogInformation("Project unsuccessfully added");
                StatusMessage = String.Format("Unable to add project");
                return RedirectToPage("./ManageProjects");
            }



        }

    }
}