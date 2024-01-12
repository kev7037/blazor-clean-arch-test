namespace Mc2.CrudTest.Presentation.Shared.HelperClasses
{
    public interface IHasDomainEvents
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
        public void ClearDomainEvents();
    }
}
