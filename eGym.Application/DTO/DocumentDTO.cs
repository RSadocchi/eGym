using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Core.Domain;
using System;

namespace eGym.Application.DTO
{
    public class DocumentDTO
    {
        public int Doc_ID { get; set; }
        public DateTime Doc_CreationDate { get; set; }
        public string Doc_Description { get; set; }
        public DateTime? Doc_IssueDate { get; set; }
        public DateTime? Doc_ExpiringDate { get; set; }
        public string Doc_Number { get; set; }
        public string Doc_EmissionCountry { get; set; }
        public string Doc_EmissionCity { get; set; }
        public string Doc_EmissionNote { get; set; }
        public string Doc_EmitterName { get; set; }
        public string Doc_Path { get; set; }
        public short Doc_TypeID { get; set; }
        public short? Doc_EmitterID { get; set; }
        public int Doc_AnagID { get; set; }

        public string EmitterLabel { get; set; }
        public string TypeLabel { get; set; }

        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;

                CreateMap<Anag_Document, DocumentDTO>()
                    .ForMember(m => m.EmitterLabel, o => o.MapFrom(s => s.Doc_EmitterID.HasValue ? EN_DocumentEmitter.FromID(s.Doc_EmitterID.Value).Code : null))
                    .ForMember(m => m.TypeLabel, o => o.MapFrom(s => EN_DocumentType.FromID(s.Doc_TypeID).Code));

                CreateMap<DocumentDTO, Anag_Document>()
                    .EqualityComparison((s, d) => s.Doc_ID != 0 && s.Doc_ID == d.Doc_ID)
                    .ForMember(m => m.Doc_ID, o => o.Ignore());
            }
        }
    }
}
