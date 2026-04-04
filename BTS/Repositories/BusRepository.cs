using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace BTS.Repositories
{
    public class BusRepository : Repository<Buses>, IBusRepository
    {
        private ApplicationDbContext _db;
        public BusRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Buses buses)
        {
            var obj = _db.Buses.FirstOrDefault(b =>b.BusId == buses.BusId);
            if (obj != null) {
            
                obj.BusNumber = buses.BusNumber;
                obj.BusName = buses.BusName;
                obj.PricePerKm = buses.PricePerKm;
                obj.PlateNumber = buses.PlateNumber;
                obj.TotalSeats = buses.TotalSeats;
                obj.BusTypeId = buses.BusTypeId;
                obj.BusCompanyId = buses.BusCompanyId;
            }

        }
    }
}
