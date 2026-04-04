using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class RouteRepository:Repository<BusRoutes>, IRouteRepository
    {
        private ApplicationDbContext _db;
        public RouteRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;   
        }

        public void Update(BusRoutes routes)
        {
            var obj = _db.BusRoutes.FirstOrDefault(u => u.RouteId == routes.RouteId);
            if(obj != null)
            {
                obj.StationId = routes.StationId;
                obj.DestinationId = routes.DestinationId;
                obj.BasePrice = routes.BasePrice;
                obj.DistanceKM = routes.DistanceKM;
                obj.EstimatedTime = routes.EstimatedTime;
                obj.IsActive = routes.IsActive;
            }
                
        }
    }
}
