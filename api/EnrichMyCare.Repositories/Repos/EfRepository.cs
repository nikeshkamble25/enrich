using EnrichMyCare.DataEntities.Entities;
using EnrichMyCare.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnrichMyCare.Repositories.Contracts
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseDataEntity
    {
        #region Fields

        private IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public EfRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        public async Task AddAsync(T entity)
            => await _unitOfWork.DbContext.Set<T>().AddAsync(entity);
        
        public async Task UpdateAsync(T entity)
            => await Task.Run(() => _unitOfWork.DbContext.Entry<T>(entity).State = EntityState.Modified);
        
        public async Task RemoveAsync(T entity)
            => await Task.Run(() => _unitOfWork.DbContext.Set<T>().Remove(entity));
        
        public async Task<IEnumerable<T>> GetAllAsync()
            => await _unitOfWork.DbContext.Set<T>().ToListAsync();
        
        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
            => await _unitOfWork.DbContext.Set<T>().Where(predicate).ToListAsync();

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => await _unitOfWork.DbContext.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> GetByIdAsync(int id) => await _unitOfWork.DbContext.Set<T>().FindAsync(id);

        #endregion
    }
}
