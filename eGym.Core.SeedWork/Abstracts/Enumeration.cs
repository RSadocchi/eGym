using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace eGym.Core.SeedWork
{
    public class Enumeration<TKey> where TKey : IEquatable<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual TKey ID { get; set; }
        public virtual string Code { get; set; }

        public Enumeration() { }
        public Enumeration(TKey id, string code) { ID = id; Code = code; }

        public static T FromID<T>(TKey id) where T : Enumeration<TKey>, new()
            => Parse<T>(id, o => o.ID.Equals(id));

        public static T FromCode<T>(string name) where T : Enumeration<TKey>, new()
            => Parse<T>(name, o => o.Code.Equals(name));

        public static T Parse<T>(object value, Func<T, bool> predicate) where T : Enumeration<TKey>, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' not found on {typeof(T)}");
            return matchingItem;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TKey>, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (var info in fields)
            {
                var instance = new T();
                if (info.GetValue(instance) is T locatedValue) yield return locatedValue;
            }
        }

        public override string ToString() => $"{ID} {Code}";

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return ID.Equals(((Enumeration<TKey>)obj).ID);
        }

        public override int GetHashCode() => ID.GetHashCode();
    }
}
