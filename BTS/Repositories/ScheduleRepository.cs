using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class ScheduleRepository:Repository<Schedules>,IScheduleRepository
    {
        private ApplicationDbContext _db;
        public ScheduleRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;   
        }

        public void Update(Schedules schedules)
        {
            var obj = _db.Schedules.FirstOrDefault(u => u.ScheduleId == schedules.ScheduleId);
            if(obj != null)
            {
               obj.RouteId = schedules.RouteId;
               obj.BusId = schedules.BusId;
               obj.DepartureTime = schedules.DepartureTime;
               obj.ArrivalTime = schedules.ArrivalTime;
               obj.Status = schedules.Status;
               obj.AvailableSeats = schedules.AvailableSeats;
            
            }
                
        }
    }
}
