using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace BTS.Repositories
{
    public class TicketRepository : Repository<Tickets>, ITicketRepository
    {
        private ApplicationDbContext _db;
        public TicketRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Tickets tickets)
        {
            var obj = _db.Tickets.FirstOrDefault(b => b.TicketId == tickets.TicketId);
            if (obj != null)
            {
                obj.TicketCode = tickets.TicketCode;
                obj.UserId = tickets.UserId;
                obj.SeatId = tickets.SeatId;
                obj.ScheduledId = tickets.ScheduledId;
                obj.AmountPaid = tickets.AmountPaid;
                obj.Status = tickets.Status;
                obj.PaymentStatus = tickets.PaymentStatus;
                obj.BookedAt = tickets.BookedAt;
                obj.CancelledAt = tickets.CancelledAt;
            }
        }
    }
}
