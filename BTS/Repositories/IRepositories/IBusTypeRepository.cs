using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface IBusTypeRepository:IRepository<BusType>
    {
        void Update(BusType busType);
    }
}
