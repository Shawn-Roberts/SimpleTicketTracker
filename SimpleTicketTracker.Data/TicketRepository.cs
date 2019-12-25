using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleTicketTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTicketTracker.Data
{
    public class TicketRepository : ITicketRepository
    {

        //FIELDS
        private readonly SimpleTicketTrackerDbContext _context;
        private readonly ILogger<TicketRepository> _logger;

        //CONSTRUCTORS
        public TicketRepository(SimpleTicketTrackerDbContext context, ILogger<TicketRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        //METHODS


        //GENERICS
        public async Task<int> Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating an object of type {entity.GetType()} to the context.");
            _context.Attach(entity).State = EntityState.Modified;
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");
            return (await _context.SaveChangesAsync()) > 0;
        }

        //PROJECTS
        public async Task<Project[]> GetAllProjectsAsync()
        {
            _logger.LogInformation($"Getting all Projects");
            var query = _context.Projects;
            return await query.ToArrayAsync();
        }

        public async Task<Project[]> GetAllProjectsAsync(string searchString)
        {
            _logger.LogInformation($"Getting filtered project");
            var query = _context.Projects.Where(p => p.Name.ToUpper().Contains(searchString.ToUpper()));
            return await query.ToArrayAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int projectID)
        {
            _logger.LogInformation($"Getting project by id {projectID}");
            var query = _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectID);
            return await query;
        }

        //TICKETS
        public async Task<Ticket[]> GetTicketByImportanceAsync(int priority)
        {
            var query = _context.Tickets.Where(t => t.PriorityId == priority)
                .Where(t => t.StatusId == 1 || t.StatusId == 2)
                .OrderBy(t => t.DueDate);
            return await query.ToArrayAsync();

        }

        public async Task<Ticket[]> GetTicketByImportanceAsync(int priority, int count)
        {
            var query = _context.Tickets.Where(t => t.PriorityId == priority)
                .Where(t => t.StatusId == 1 || t.StatusId == 2)
                .OrderBy(t => t.DueDate)
                .Take(count);
            return await query.ToArrayAsync();

        }

        public async Task<Ticket[]> GetAllTicketsAsync()
        {
            _logger.LogInformation($"Getting all tickets");
            var query = _context.Tickets.Where(t => t.StatusId == 1 || t.StatusId == 2);
            return await query.ToArrayAsync();
        }

        public Task<Ticket[]> GetAllTicketsAsync(string searchString)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            var query = _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
            return await query;
        }

        public int GetTicketCount(int priority)
        {
            _logger.LogInformation($"Getting count ofall {0} tickets", priority);
            var count = _context.Tickets.Where(t => t.PriorityId == priority)
                .Where(t => t.StatusId == 1 || t.StatusId == 2)
                .Count();
            return count;
        }

        //COMMENTS
        public async Task<IEnumerable<TicketComment>> GetTicketCommentsByIdAsync(int ticketId)
        {
            var query = _context.Comments.Where(c => c.TicketID == ticketId).ToListAsync(); ;
            return await query;
        }

        public async Task<int> AddTicketCommentAsync(string comment, int ticketID)
        {
            _logger.LogInformation($"Adding an comment to ticket {0}", ticketID);
            var ticketComment = new TicketComment { TicketID = ticketID, Comment = comment, CommentorId = 1, CommentDate = DateTime.UtcNow };
            _context.Add(ticketComment);
            return await _context.SaveChangesAsync();
        }


        //DROPDOWNS
        public async Task<IEnumerable<SelectListItem>> GetProjectsDropdownAsync()
        {
            var query = _context.Projects.Select(p =>
                               new SelectListItem
                               {
                                   Value = p.ProjectId.ToString(),
                                   Text = p.Name
                               }).ToListAsync();
            return await query;
        }

        public async Task<IEnumerable<SelectListItem>> GetPriorityDropdownAsync()
        {
            var query = _context.Priority.Select(p =>
                    new SelectListItem
                    {
                        Value = p.PriorityId.ToString(),
                        Text = p.Name
                    }).ToListAsync();
            return await query;
        }

        public async Task<IEnumerable<SelectListItem>> GetUsersDropdownAsync()
        {
            var query = _context.Users.Select(p =>
          new SelectListItem
          {
              Value = p.Id.ToString(),
              Text = p.FirstName + "," + p.LastName
          }).ToListAsync();
            return await query;
        }

        public async Task<IEnumerable<SelectListItem>> GetRequestTypeDropdownAsync()
        {
            var query = _context.Type.Select(p =>
                new SelectListItem
                {
                    Value = p.TypeId.ToString(),
                    Text = p.Type
                }).ToListAsync();
            return await query;
        }

        public async Task<IEnumerable<SelectListItem>> GetTicketStatusDropdownAsync()
        {
            var query = _context.Status.Select(p =>
                new SelectListItem
                {
                    Value = p.StatusId.ToString(),
                    Text = p.Description
                }).ToListAsync();
            return await query;
        }

        //OVERVIEW
        public int GetProjectCount()
        {
            var query = _context.Projects.AsEnumerable().Count();
            return query;
        }

        public int GetTicketCount()
        {
            var query = _context.Tickets.AsEnumerable().Count();
            return query;
        }



        //DETAILS PAGE CONVERSIONS

        public string GetProjectName(int projectId)
        {
            var result = _context.Projects.Where(p => p.ProjectId == projectId).FirstOrDefault();
            return result.Name;
        }

        public string GetTickerOwnerName(int ownerId)
        {
            var result = _context.Users.Where(p => p.Id == ownerId).FirstOrDefault();
            return string.Format("{0},{1}", result.FirstName, result.LastName);
        }

        public string GetTicketPriorityName(int priorityId)
        {
            var result = _context.Priority.Where(p => p.PriorityId == priorityId).FirstOrDefault();
            return result.Name;
        }

        public string GetTicketRequestTypeName(int requestId)
        {
            var result = _context.Type.Where(t => t.TypeId == requestId).FirstOrDefault();
            return result.Type;
        }

        public string GetRequestStatusName(int statusId)
        {
            var result = _context.Status.Where(s => s.StatusId == statusId).FirstOrDefault();
            return result.Name;
        }

        public async Task<Ticket[]> GetTicketsByFilterAsync(int? statusFilter)
        {
            var query = _context.Tickets.Where(t => t.StatusId == statusFilter);
            return await query.ToArrayAsync();
        }


    }
}
