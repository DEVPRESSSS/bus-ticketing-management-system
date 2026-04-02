using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface IBusCompanyRepository:IRepository<BusCompanies>
    {
        void Update(BusCompanies busesCompanies);
    }
}
