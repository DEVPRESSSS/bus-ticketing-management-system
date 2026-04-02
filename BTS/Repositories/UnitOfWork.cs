using BTS.Data;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IBusRepository Bus { get; private set; }
        public IBusCompanyRepository BusCompany { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Bus = new BusRepository(db);
            BusCompany = new BusCompanyRepository(db);
        }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
