using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Core.Domain;

namespace eGym.Application.DTO
{
    public class ContactDTO
    {
        public int Cnt_ID { get; set; }
        public string Cnt_Value { get; set; }
        public bool Cnt_DefaultInType { get; set; }
        public string Cnt_Note { get; set; }
        public int Cnt_AnagID { get; set; }
        public short Cnt_TypeID { get; set; }

        public string TypeLabel { get; set; }

        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;

                CreateMap<Anag_Contact, ContactDTO>()
                    .ForMember(m => m.TypeLabel, o => o.MapFrom(s => EN_ContactType.FromID(s.Cnt_TypeID).Code));

                CreateMap<ContactDTO, Anag_Contact>()
                    .EqualityComparison((s, d) => s.Cnt_ID != 0 && s.Cnt_ID == d.Cnt_ID)
                    .ForMember(m => m.Cnt_ID, o => o.Ignore());
            }
        }
    }
}
