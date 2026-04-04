using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface ISeatRepository: IRepository<Seats>
    {
        void Update(Seats seats);
    }
}
