namespace Mc2.CrudTest.Presentation.Shared.HelperClasses
{
    public class BaseEntity : IHasDomainEvents
    {
        public long Id { get; set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent @domainEvent)
            => _domainEvents.Add(@domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
