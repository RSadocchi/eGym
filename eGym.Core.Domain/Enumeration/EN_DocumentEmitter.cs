using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
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
        public static EN_DocumentEmitter SportsInstitution = new EN_DocumentEmitter(7, nameof(SportsInstitution));
        public static EN_DocumentEmitter EducationalInstitution = new EN_DocumentEmitter(8, nameof(EducationalInstitution));
        public static EN_DocumentEmitter SportsFederation = new EN_DocumentEmitter(9, nameof(SportsFederation));
    }
}
