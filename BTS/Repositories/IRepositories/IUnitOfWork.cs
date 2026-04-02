namespace BTS.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IBusRepository Bus { get; }
        IBusCompanyRepository BusCompany { get; }
        void Save();
    }
}
