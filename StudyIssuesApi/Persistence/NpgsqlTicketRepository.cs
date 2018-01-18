using System;
using StudyIssuesApi.Models;

namespace StudyIssuesApi.Persistence
{
    public class NpgsqlTicketRepository : ITicketRepository
    {
        public Func<TicketDbContext> getDbContext { get; set; }

        public void AddTicket(Ticket ticket)
        {
            using(TicketDbContext context = getDbContext())
            {
                context.Add(ticket);
                context.SaveChanges();
            }
        }
    }
}