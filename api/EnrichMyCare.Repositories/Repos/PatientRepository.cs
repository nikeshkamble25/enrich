using EnrichMyCare.DataEntities;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnrichMyCare.Repositories.Repos
{
    public class PatientRepository : EfRepository<Patient>, IPatientRepository
    {
        public PatientRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        { }
    }
}
