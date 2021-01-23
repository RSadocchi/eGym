using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eGym.Core.SeedWork
{
    public abstract class Entity
    {
        //int? _requestedHashCode;
        //private List<INotification> _domainEvents;
        //public List<INotification> DomainEvents => _domainEvents;

        //public void AddDomainEvent(INotification eventItem)
        //{
        //    _domainEvents = _domainEvents ?? new List<INotification>();
        //    _domainEvents.Add(eventItem);
        //}

        //public void RemoveDomainEvent(INotification eventItem)
        //{
        //    if (_domainEvents is null) return;
        //    _domainEvents.Remove(eventItem);
        //}

        [Timestamp]
        public byte[] ConcurrencyTimestamp { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity)) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null)) return (Object.Equals(right, null)) ? true : false;
            else return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
