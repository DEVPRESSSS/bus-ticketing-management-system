using BTS.Data;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IBusRepository Bus { get; private set; }
        public IBusCompanyRepository BusCompany { get; private set; }
        public IBusTypeRepository BusType { get; private set; }
        public ISeatRepository Seat { get; private set; }
        public IStationRepository Station { get; private set; }
        public IRouteRepository BusRoute { get; private set; }
        public IScheduleRepository Schedule { get; private set; }
        public ITicketRepository Ticket { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Bus = new BusRepository(db);
            BusCompany = new BusCompanyRepository(db);
            BusType = new BusTypeRepository(db);
            Seat = new SeatRepository(db);
            Station = new StationRepository(db);
            BusRoute = new RouteRepository(db);
            Schedule = new ScheduleRepository(db);
            Ticket = new TicketRepository(db);
        }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
