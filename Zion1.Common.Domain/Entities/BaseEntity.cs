using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zion1.Common.Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T? Id { get; set; }

        public int  ClientId { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            DomainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            DomainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            DomainEvents.Clear();
        }
    }
}