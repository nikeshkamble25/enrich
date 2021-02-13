using EnrichMyCare.DataEntities;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;

namespace EnrichMyCare.Repositories.Repos
{
    public class ImageVideoRepository : EfRepository<ImageVideo>, IImageVideoRepository
    {
        public ImageVideoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
