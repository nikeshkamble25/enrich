using EnrichMyCare.EnrichDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnrichMyCare.Repositories.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        EnrichMyCareContext DbContext { get; }
    }
}
