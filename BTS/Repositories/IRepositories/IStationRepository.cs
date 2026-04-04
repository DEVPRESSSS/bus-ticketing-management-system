using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface IStationRepository: IRepository<Stations>
    {
        void Update(Stations stations);
    }
}
