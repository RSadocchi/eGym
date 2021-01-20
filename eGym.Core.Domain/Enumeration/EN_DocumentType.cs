using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_DocumentType : Enumeration<short>
    {
        public EN_DocumentType() : base() { }
        public EN_DocumentType(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_DocumentType> GetAll() => GetAll<EN_DocumentType>();
        public static EN_DocumentType FromID(short id) => FromID<EN_DocumentType>(id);

        public static EN_DocumentType IdCard = new EN_DocumentType(1, nameof(IdCard));
        public static EN_DocumentType FiscalCode = new EN_DocumentType(2, nameof(FiscalCode));
        public static EN_DocumentType SportsMedicalCertificate = new EN_DocumentType(3, nameof(SportsMedicalCertificate));
        public static EN_DocumentType CompetitiveMedicalCertificate = new EN_DocumentType(4, nameof(CompetitiveMedicalCertificate));
        public static EN_DocumentType MedicalCertificate = new EN_DocumentType(5, nameof(MedicalCertificate));
        public static EN_DocumentType MedicalReport = new EN_DocumentType(6, nameof(MedicalReport));
        public static EN_DocumentType Certificate = new EN_DocumentType(7, nameof(Certificate));
        public static EN_DocumentType Graduation = new EN_DocumentType(8, nameof(Graduation));
    }
}
