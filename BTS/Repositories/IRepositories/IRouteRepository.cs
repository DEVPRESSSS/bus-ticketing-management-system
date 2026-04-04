using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface IRouteRepository: IRepository<BusRoutes>
    {
        void Update(BusRoutes route);
    }
}
