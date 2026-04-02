using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface IBusRepository: IRepository<Buses>
    {
        void Update(Buses buses);
    }
}
