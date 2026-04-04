using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class SeatRepository:Repository<Seats>, ISeatRepository
    {
        private ApplicationDbContext _db;
        public SeatRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;   
        }

        public void Update(Seats seats)
        {
            var obj = _db.Seats.FirstOrDefault(u => u.SeatId == seats.SeatId);
            if(obj != null)
            {
               obj.SeatNumber = seats.SeatNumber;
               obj.BusId = seats.BusId;
               obj.SeatNumber = seats.SeatNumber;
               obj.isActive = seats.isActive;
            }
                
        }
    }
}
