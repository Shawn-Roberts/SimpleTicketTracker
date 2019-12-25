using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleTicketTracker.Core.Identity;
using SimpleTicketTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTicketTracker.Data
{
    public class SimpleTicketTrackerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public SimpleTicketTrackerDbContext(DbContextOptions<SimpleTicketTrackerDbContext> options):base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<TicketType> Type { get; set; }
        public DbSet<TicketComment> Comments { get; set; }
    }
}
