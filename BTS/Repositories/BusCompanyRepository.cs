using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class BusCompanyRepository: Repository<BusCompanies>, IBusCompanyRepository
    {
        private ApplicationDbContext _db;
        public BusCompanyRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(BusCompanies busesCompanies)
        {
            var obj = _db.BusCompanies.FirstOrDefault(b => b.BusCompanyId == busesCompanies.BusCompanyId);
            if (obj != null)
            {
                obj.CompanyName = busesCompanies.CompanyName;
                obj.ContactNumber = busesCompanies.ContactNumber;
                obj.Email = busesCompanies.Email;
                obj.Address = busesCompanies.Address;

            }
        }
    }
}
