using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class StationRepository:Repository<Stations>,IStationRepository
    {
        private ApplicationDbContext _db;
        public StationRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;   
        }

        public void Update(Stations stations)
        {
            var obj = _db.Stations.FirstOrDefault(u => u.StationId == stations.StationId);
            if(obj != null)
            {
               obj.StationName = stations.StationName;
               obj.City = stations.City;
               obj.Province = stations.Province;
               obj.Addresss = stations.Addresss;
               obj.Latitude = stations.Latitude;
               obj.Longitude = stations.Longitude;
               obj.isActive = stations.isActive;
            }
                
        }
    }
}
