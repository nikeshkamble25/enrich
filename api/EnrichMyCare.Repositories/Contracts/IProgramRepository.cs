using EnrichMyCare.DataEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrichMyCare.Repositories.Contracts
{
    public interface IProgramRepository : IAsyncRepository<Program>
    {
        Task<IEnumerable<Program>> GetProgramByPatientId(int patientId);
    }
}
