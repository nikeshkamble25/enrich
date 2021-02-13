using EnrichMyCare.EnrichDatabase.Entities;
using System.Threading.Tasks;

namespace EnrichMyCare.Repositories.Infrastructure
{
    /// <summary>
    /// This concerte implementation is used for handling the Transaction across repositories
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(EnrichMyCareContext dbContext)
        {
            DbContext = dbContext;
        }

        public EnrichMyCareContext DbContext { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
