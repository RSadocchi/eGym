using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_Gender : Enumeration<short>
    {
        public EN_Gender() : base() { }
        public EN_Gender(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_Gender> GetAll() => GetAll<EN_Gender>();
        public static EN_Gender FromID(short id) => FromID<EN_Gender>(id);
        public static EN_Gender FromCode(string code) => FromCode<EN_Gender>(code);

        public static EN_Gender Undefined = new EN_Gender(0, nameof(Undefined));
        public static EN_Gender Male = new EN_Gender(1, nameof(Male));
        public static EN_Gender Female = new EN_Gender(2, nameof(Female));
    }
}
