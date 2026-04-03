using BTS.Data;
using BTS.Models;
using BTS.Repositories.IRepositories;

namespace BTS.Repositories
{
    public class BusTypeRepository:Repository<BusType>, IBusTypeRepository
    {
        private ApplicationDbContext _db;
        public BusTypeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;   
        }

        public void Update(BusType busType)
        {
            var obj = _db.BusType.FirstOrDefault(u => u.BusTypeId == busType.BusTypeId);
            if(obj != null)
            {
                obj.BusTypeName = busType.BusTypeName;
            }
                
        }
    }
}
