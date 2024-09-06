using Banha_UniverCity.Data;
using Banha_UniverCity.Repository.IRepository;

namespace Banha_UniverCity.Repository.ModelsRepository
{
    public class AcademicYearRepository : GenralRepository<AcademicYearRepository>, IAcademicYearRepository
    {
        public AcademicYearRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
