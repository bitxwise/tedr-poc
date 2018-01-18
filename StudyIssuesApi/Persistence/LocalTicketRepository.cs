using System;
using System.Collections.Generic;
using StudyIssuesApi.Models;

namespace StudyIssuesApi.Persistence
{
    public class LocalTicketRepository : ITicketRepository
    {
        private readonly Dictionary<Guid, Ticket> _storage = new Dictionary<Guid, Ticket>();

        public void AddTicket(Ticket ticket)
        {
            if(!_storage.ContainsKey(ticket.Id))
                _storage.Add(ticket.Id, ticket);
        }
    }
}