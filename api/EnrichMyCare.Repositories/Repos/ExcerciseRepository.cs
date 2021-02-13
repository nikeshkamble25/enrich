using EnrichMyCare.DataEntities;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrichMyCare.Repositories.Repos
{
    public class ExcerciseRepository : EfRepository<Excercise>, IExcerciseRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcerciseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Excercise>> GetExerciseByProgramId(int programId)
        {
            return await _unitOfWork.DbContext.Excercise
                               .Where(x => x.ProgramId == programId)
                               .Include(x => x.InitialProgression)
                               .ThenInclude(e => e.Progression)
                               .Include(x => x.ImageVideo)
                               .ToArrayAsync();
        }
    }
}
