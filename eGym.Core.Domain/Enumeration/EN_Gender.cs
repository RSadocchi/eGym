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

    public partial class EN_DocumentEmitter : Enumeration<short>
    {
        public EN_DocumentEmitter() { }
        public EN_DocumentEmitter(short id, string code) : base(id, code) { }


        public static IEnumerable<EN_DocumentEmitter> GetAll() => GetAll<EN_DocumentEmitter>();
        public static EN_DocumentEmitter FromID(short id) => FromID<EN_DocumentEmitter>(id);


        public static EN_DocumentEmitter Municipality = new EN_DocumentEmitter(1, nameof(Municipality));
        public static EN_DocumentEmitter Prefecture = new EN_DocumentEmitter(2, nameof(Prefecture));
        public static EN_DocumentEmitter PoliceHeadquarters = new EN_DocumentEmitter(3, nameof(PoliceHeadquarters));
        public static EN_DocumentEmitter Hospital = new EN_DocumentEmitter(4, nameof(Hospital));
        public static EN_DocumentEmitter Doctor = new EN_DocumentEmitter(5, nameof(Doctor));
        public static EN_DocumentEmitter SportsDoctor = new EN_DocumentEmitter(6, nameof(SportsDoctor));
    }
}
