using EnrichMyCare.DataEntities;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;

namespace EnrichMyCare.Repositories.Repos
{
    public class PatientProgramRepository : EfRepository<PatientProgram>, IPatientProgramRepository
    {
        public PatientProgramRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
