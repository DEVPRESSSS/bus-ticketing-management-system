using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface ITicketRepository: IRepository<Tickets>
    {
        void Update(Tickets ticket);
    }
}
