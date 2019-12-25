using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleTicketTracker.Core.Models;
using SimpleTicketTracker.Data;
using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SimpleTicketTracker
{
    public class OverviewModel : PageModel
    {

        //FIELDS
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<OverviewModel> _logger;

        public string ChartJson { get; internal set; }

        public DataTable ProjectData { get; set; }





        public int UserCount { get; set; }
        public int ProjectCount { get; set; }

        public int TicketCount { get; set; }

        //CONSTRUCTORS
        public OverviewModel(ITicketRepository ticketRepository, IUserRepository userRepository, ILogger<OverviewModel> logger)
        {
            this._ticketRepository = ticketRepository;
            this._userRepository = userRepository;
            this._logger = logger;
        }

        //METHODS
        public async Task<IActionResult> OnGetAsync()
        {

            UserCount = _userRepository.GetUserCount();
            ProjectCount = _ticketRepository.GetProjectCount();
            TicketCount = _ticketRepository.GetTicketCount();
            

            _logger.LogInformation("Getting charting information");
            // Hard Coded Values.. Can move to linq query eventually
            ProjectData = new DataTable();
            ProjectData.Columns.Add("Project Name", typeof(System.String));
            ProjectData.Columns.Add("Hours Worked", typeof(System.Double));
            ProjectData.Rows.Add("First Project", 100);
            ProjectData.Rows.Add("Second Project", 300);
            ProjectData.Rows.Add("Third Project", 200);
            ProjectData.Rows.Add("Fourth Project", 800);
            StaticSource source = new StaticSource(ProjectData);
            DataModel model = new DataModel();
            model.DataSources.Add(source);
            Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
            column.Width.Em(30);
            column.Height.Em(30);
            column.Data.Source = model;
            column.Caption.Text = "Hours spent on projects";
            column.Legend.Show = true;
            column.XAxis.Text = "Project Name";
            column.YAxis.Text = "Hours Worked";
            column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            ChartJson = column.Render();
            Debug.Write(ChartJson);

            return Page();

        }
    }
}