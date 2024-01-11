using Mc2.CrudTest.Core.Domain;

namespace Mc2.CrudTest.Infrastructures.Command
{
    public class CustomerUnitOfWork : BaseEntityFrameworkUnitOfWork<CommandDBContext>, ICustomerUnitOfWork
    {
        public CustomerUnitOfWork(CommandDBContext dbContext) : base(dbContext)
        {
        }
    }
}
