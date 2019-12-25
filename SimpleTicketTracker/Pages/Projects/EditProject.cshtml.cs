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
    public class EditProjectModel : PageModel
    {
        //FIELDS
        private readonly ITicketRepository _projectData;
        private readonly ILogger<Project> _logger;

        [BindProperty]
        public Project Project { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        //CONSTRUCTORS
        public EditProjectModel(ITicketRepository context, ILogger<Project> logger)
        {
            this._projectData = context;
            this._logger = logger;
        }

        //METHODS

        public async Task<IActionResult> OnGetAsync(int projectId)
        {
            Project = await _projectData.GetProjectByIdAsync(projectId);

            if (Project == null)
            {
                _logger.LogInformation("Unable to find project");
                return RedirectToPage("../Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Project = await _projectData.GetProjectByIdAsync(Project.ProjectId);
                StatusMessage = String.Format("Error updating project");
                return Page();
            }
            if (await _projectData.Update(Project) > 0)
            {
                _logger.LogInformation("Project sucessfully updated");
                StatusMessage = String.Format("Project successfully updated");
                return RedirectToPage("./ManageProjects");

            }
            else
            {
                _logger.LogInformation("Project unsuccessfully updated");
                StatusMessage = String.Format("Unable to update project");
                return RedirectToPage("./ManageProjects");
            }


        }
    }
}