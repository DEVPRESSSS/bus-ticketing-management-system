using BTS.Models;

namespace BTS.Repositories.IRepositories
{
    public interface IScheduleRepository: IRepository<Schedules>
    {
        void Update(Schedules schedules);
    }
}
