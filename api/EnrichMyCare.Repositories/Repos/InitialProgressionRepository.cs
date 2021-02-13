using EnrichMyCare.DataEntities;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnrichMyCare.Repositories.Repos
{
    public class InitialProgressionRepository : EfRepository<InitialProgression>, IInitialProgressionRepository
    {
        public InitialProgressionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
