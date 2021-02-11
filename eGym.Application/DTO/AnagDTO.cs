using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGym.Application.DTO
{
    public class AnagDTO
    {
        public int Ang_ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Ang_FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Ang_LastName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Ang_TaxCode { get; set; }
        [MaxLength(30)]
        public string Ang_VATNo { get; set; }
        public DateTime? Ang_BirthDate { get; set; }
        [MaxLength(150)]
        public string Ang_BirthCity { get; set; }
        [MaxLength(5)]
        public string Ang_BirthCountrySpec { get; set; }
        [MaxLength(3)]
        public string Ang_BirthCountry { get; set; }
        [MaxLength(3)]
        public string Ang_Citizenship { get; set; }
        public string Ang_Avatar { get; set; }
        public string Ang_Note { get; set; }
        public short Ang_GenderID { get; set; }
        public int? Ang_UserID { get; set; }

        public List<AddressDTO> Anag_Addresses { get; set; } = new List<AddressDTO>();
        public List<ContactDTO> Anag_Contacts { get; set; } = new List<ContactDTO>();
        public List<DynamicRoleDTO> Anag_MasterRoles { get; set; } = new List<DynamicRoleDTO>();
        public List<DynamicRoleDTO> Anag_CorporateRoles { get; set; } = new List<DynamicRoleDTO>();
        public List<DocumentDTO> Anag_Documents { get; set; } = new List<DocumentDTO>();

        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;

                CreateMap<Anag_Master, AnagDTO>();

                CreateMap<AnagDTO, Anag_Master>()
                    .EqualityComparison((s, d) => s.Ang_ID != 0 && s.Ang_ID == d.Ang_ID)
                    .ForMember(m => m.Ang_ID, o => o.Ignore());
            }
        }
    }
}
