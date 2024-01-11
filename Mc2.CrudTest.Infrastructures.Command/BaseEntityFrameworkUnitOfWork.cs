using Mc2.CrudTest.Core.Domain;

namespace Mc2.CrudTest.Infrastructures.Command
{
    public abstract class BaseEntityFrameworkUnitOfWork<TDbContext> : ICustomerUnitOfWork where TDbContext : CommandDBContext
    {
        protected readonly TDbContext _dbContext;

        public BaseEntityFrameworkUnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}