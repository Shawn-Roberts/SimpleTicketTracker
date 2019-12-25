using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleTicketTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTicketTracker.Data
{
    public interface ITicketRepository
    {
        //GENERICS
        Task<int> Add<T>(T entity) where T : class;
        Task<int> Update<T>(T entity) where T : class;
        Task<int> Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();



        //PROJECTS
        Task<Project[]> GetAllProjectsAsync();
        Task<Project[]> GetAllProjectsAsync(string searchString);

        Task<Project> GetProjectByIdAsync(int projectId);



        //TICKETS
        Task<Ticket[]> GetAllTicketsAsync();

        int GetTicketCount(int priority);
        Task<Ticket[]> GetAllTicketsAsync(string searchString);
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<Ticket[]> GetTicketByImportanceAsync(int priority);
        Task<Ticket[]> GetTicketByImportanceAsync(int priority, int count);

        Task<Ticket[]> GetTicketsByFilterAsync(int? statusFilter);

        //COMMENTS
        Task<IEnumerable<TicketComment>> GetTicketCommentsByIdAsync(int ticketId);

        Task<int> AddTicketCommentAsync(string comment, int ticketID);



        //DROP DOWN ENUMERABLES
        Task<IEnumerable<SelectListItem>> GetProjectsDropdownAsync();
        Task<IEnumerable<SelectListItem>> GetPriorityDropdownAsync();

        Task<IEnumerable<SelectListItem>> GetUsersDropdownAsync();
        Task<IEnumerable<SelectListItem>> GetRequestTypeDropdownAsync();
        Task<IEnumerable<SelectListItem>> GetTicketStatusDropdownAsync();


        //OVER VIEW STATS
        int GetProjectCount();
        int GetTicketCount();


        //DETAILS OVER VIEW
        string GetProjectName(int projectId);
        string GetTickerOwnerName(int ownerId);
        string GetTicketPriorityName(int priorityId);

        string GetTicketRequestTypeName(int requestId);

        string GetRequestStatusName(int statusId);





    }

}
