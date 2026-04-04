namespace BTS.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IBusRepository Bus { get; }
        IBusCompanyRepository BusCompany { get; }
        IBusTypeRepository BusType { get; }
        ISeatRepository Seat { get; }
        IStationRepository Station { get; }
        IRouteRepository BusRoute { get; }
        IScheduleRepository Schedule { get; }
     
        void Save();
    }
}
