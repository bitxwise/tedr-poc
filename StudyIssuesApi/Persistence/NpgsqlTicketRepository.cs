using StudyIssuesApi.Models;

namespace StudyIssuesApi.Persistence
{
    public class NpgsqlTicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _context;

        public NpgsqlTicketRepository(TicketDbContext context)
        {
            _context = context;
        }

        public void AddTicket(Ticket ticket)
        {
            _context.Add(ticket);
        }
    }
}