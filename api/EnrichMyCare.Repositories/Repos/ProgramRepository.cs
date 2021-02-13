using EnrichMyCare.DataEntities;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrichMyCare.Repositories.Repos
{
    public class ProgramRepository : EfRepository<Program>, IProgramRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Program>> GetProgramByPatientId(int patientId)
        {
            return await (from prog in _unitOfWork.DbContext.Program
                          join progPatient in _unitOfWork.DbContext.PatientProgram
                          on prog.ProgramId equals progPatient.ProgramId
                          where progPatient.PatientId == patientId
                          select prog).ToListAsync();
        }
    }
}
