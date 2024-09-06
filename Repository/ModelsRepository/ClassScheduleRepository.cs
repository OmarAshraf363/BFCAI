using Banha_UniverCity.Data;
using Banha_UniverCity.Repository.IRepository;

namespace Banha_UniverCity.Repository.ModelsRepository
{
    public class ClassScheduleRepository : GenralRepository<ClassScheduleRepository>, IClassScheduleRepository
    {
        public ClassScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
