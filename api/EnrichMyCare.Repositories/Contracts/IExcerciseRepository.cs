using EnrichMyCare.DataEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrichMyCare.Repositories.Contracts
{
    public interface IExcerciseRepository : IAsyncRepository<Excercise>
    {
        Task<IEnumerable<Excercise>> GetExerciseByProgramId(int programId);
    }
}
