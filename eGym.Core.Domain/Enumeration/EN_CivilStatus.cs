using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Core.Domain
{
    public class EN_CivilStatus : Enumeration<int>
    {
        public EN_CivilStatus() : base() { }
        public EN_CivilStatus(int id, string code) : base(id, code) { }

        public static IEnumerable<EN_CivilStatus> GetAll() => GetAll<EN_CivilStatus>();
        public static EN_CivilStatus FromID(int id) => FromID<EN_CivilStatus>(id);
        public static EN_CivilStatus FromCode(string code) => FromCode<EN_CivilStatus>(code);

        public static EN_CivilStatus CelibeNubile = new EN_CivilStatus(1, "Celibe/Nubile");
        public static EN_CivilStatus Coniugato = new EN_CivilStatus(2, "Coniugato/a");
        public static EN_CivilStatus Separato = new EN_CivilStatus(3, "Separato/a");
        public static EN_CivilStatus Divorziato = new EN_CivilStatus(4, "Divorziato/a");
        public static EN_CivilStatus Vedovo = new EN_CivilStatus(5, "Vedovo/a");
        public static EN_CivilStatus Abbandonato = new EN_CivilStatus(6, "Abbandonato/a");
    }
}
