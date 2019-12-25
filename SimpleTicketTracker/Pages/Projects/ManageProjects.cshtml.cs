using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleTicketTracker.Core.Models;
using SimpleTicketTracker.Data;

namespace SimpleTicketTracker
{
    public class ManageProjectsModel : PageModel
    {
        //FIELDS
        private readonly ITicketRepository _projectdata;

        public IEnumerable<Project> Projects;

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //CONSTRUCTORS
        public ManageProjectsModel(ITicketRepository projectdata)
        {
            this._projectdata = projectdata;
        }

        //METHODS
        public async Task<IActionResult> OnGetAsync(string? searchString)
        {
            //No on error on this as its just a search inside of the page. 0 results isnt an error.
            if (String.IsNullOrEmpty(searchString))
            {
                Projects = await _projectdata.GetAllProjectsAsync();
            }
            else
            {
                Projects = await _projectdata.GetAllProjectsAsync(searchString);
            }


            return Page();
        }
    }
}