using StudyIssuesApi.Models;

namespace StudyIssuesApi.Persistence
{
    public interface ITicketRepository
    {
        void AddTicket(Ticket ticket);
    }
}