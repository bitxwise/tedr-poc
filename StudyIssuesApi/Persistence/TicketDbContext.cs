using Microsoft.EntityFrameworkCore;
using StudyIssuesApi.Models;

namespace StudyIssuesApi.Persistence
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options) { }

        DbSet<Ticket> Tickets { get; set; }
    }
}