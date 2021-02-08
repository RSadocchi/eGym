using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Core.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGym.Application.DTO
{
    public class AddressDTO
    {
        public int Adr_ID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Adr_Address { get; set; }
        [MaxLength(250)]
        public string Adr_Address1 { get; set; }
        [MaxLength(250)]
        public string Adr_Address2 { get; set; }
        [MaxLength(10)]
        public string Adr_HouseNumber { get; set; }
        [MaxLength(10)]
        public string Adr_Staircase { get; set; }
        [MaxLength(10)]
        public string Adr_Interior { get; set; }
        [MaxLength(10)]
        public string Adr_Floor { get; set; }
        [Required]
        [MaxLength(150)]
        public string Adr_City { get; set; }
        [Required]
        [MaxLength(10)]
        public string Adr_PostalCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Adr_CountrySpec { get; set; }
        [MaxLength(50)]
        public string Adr_District { get; set; }
        [Required]
        [MaxLength(3)]
        public string Adr_Country { get; set; }
        public int Adr_AnagID { get; set; }

        public List<DynamicRoleDTO> Anag_AddressRoles { get; set; } = new List<DynamicRoleDTO>();
        public string CountryName { get; set; }

        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;

                CreateMap<Anag_Address, AddressDTO>()
                    .ForMember(m => m.CountryName, o => o.MapFrom(s => s.Country != null ? s.Country.Country_CountryName : null));

                CreateMap<AddressDTO, Anag_Address>()
                    .EqualityComparison((s, d) => s.Adr_ID != 0 && s.Adr_ID == d.Adr_ID)
                    .ForMember(m => m.Adr_ID, o => o.Ignore());
            }
        }
    }
}
