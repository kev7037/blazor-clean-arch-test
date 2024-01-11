namespace Mc2.CrudTest.Core.Domain
{
    public interface ICustomerUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync();
    }
}
