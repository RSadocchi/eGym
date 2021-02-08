using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Core.Domain;
using System;

namespace eGym.Application.DTO
{
    public class DynamicRoleDTO
    {
        public int ID { get; set; }
        public int EntityMasterID { get; set; }
        public short EnumID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string EnumLabel { get; set; }

        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;

                CreateMap<Anag_MasterRole, DynamicRoleDTO>()
                    .ForMember(m => m.ID, o => o.MapFrom(s => s.AngR_ID))
                    .ForMember(m => m.EntityMasterID, o => o.MapFrom(s => s.AngR_AnagID))
                    .ForMember(m => m.EnumID, o => o.MapFrom(s => s.AngR_RoleID))
                    .ForMember(m => m.StartDate, o => o.MapFrom(s => s.AngR_StartDate))
                    .ForMember(m => m.EndDate, o => o.MapFrom(s => s.AngR_EndDate))
                    .ForMember(m => m.EnumLabel, o => o.MapFrom(s => EN_AnagMasterRole.FromID(s.AngR_RoleID).Code));
                CreateMap<DynamicRoleDTO, Anag_MasterRole>()
                    .EqualityComparison((s, d) => s.ID != 0 && s.ID == d.AngR_ID)
                    .ForMember(m => m.AngR_ID, o => o.Ignore())
                    .ForMember(m => m.AngR_AnagID, o => o.MapFrom(s => s.EntityMasterID))
                    .ForMember(m => m.AngR_RoleID, o => o.MapFrom(s => s.EnumID))
                    .ForMember(m => m.AngR_StartDate, o => o.MapFrom(s => s.StartDate))
                    .ForMember(m => m.AngR_EndDate, o => o.MapFrom(s => s.EndDate));

                CreateMap<Anag_CorporateRole, DynamicRoleDTO>()
                    .ForMember(m => m.ID, o => o.MapFrom(s => s.CR_ID))
                    .ForMember(m => m.EntityMasterID, o => o.MapFrom(s => s.CR_AnagID))
                    .ForMember(m => m.EnumID, o => o.MapFrom(s => s.CR_RoleID))
                    .ForMember(m => m.StartDate, o => o.MapFrom(s => s.CR_StartDate))
                    .ForMember(m => m.EndDate, o => o.MapFrom(s => s.CR_EndDate))
                    .ForMember(m => m.EnumLabel, o => o.MapFrom(s => EN_CorporateRole.FromID(s.CR_RoleID).Code));
                CreateMap<DynamicRoleDTO, Anag_CorporateRole>()
                    .EqualityComparison((s, d) => s.ID != 0 && s.ID == d.CR_ID)
                    .ForMember(m => m.CR_ID, o => o.Ignore())
                    .ForMember(m => m.CR_AnagID, o => o.MapFrom(s => s.EntityMasterID))
                    .ForMember(m => m.CR_RoleID, o => o.MapFrom(s => s.EnumID))
                    .ForMember(m => m.CR_StartDate, o => o.MapFrom(s => s.StartDate))
                    .ForMember(m => m.CR_EndDate, o => o.MapFrom(s => s.EndDate));

                CreateMap<Anag_AddressRole, DynamicRoleDTO>()
                    .ForMember(m => m.ID, o => o.MapFrom(s => s.AdrR_ID))
                    .ForMember(m => m.EntityMasterID, o => o.MapFrom(s => s.AdrR_AddressID))
                    .ForMember(m => m.EnumID, o => o.MapFrom(s => s.AdrR_RoleID))
                    .ForMember(m => m.StartDate, o => o.MapFrom(s => s.AdrR_StartDate))
                    .ForMember(m => m.EndDate, o => o.MapFrom(s => s.AdrR_EndDate))
                    .ForMember(m => m.EnumLabel, o => o.MapFrom(s => EN_AddressRole.FromID(s.AdrR_RoleID).Code));
                CreateMap<DynamicRoleDTO, Anag_AddressRole>()
                    .EqualityComparison((s, d) => s.ID != 0 && s.ID == d.CR_ID)
                    .ForMember(m => m.AdrR_ID, o => o.Ignore())
                    .ForMember(m => m.AdrR_AddressID, o => o.MapFrom(s => s.EntityMasterID))
                    .ForMember(m => m.AdrR_RoleID, o => o.MapFrom(s => s.EnumID))
                    .ForMember(m => m.AdrR_StartDate, o => o.MapFrom(s => s.StartDate))
                    .ForMember(m => m.AdrR_EndDate, o => o.MapFrom(s => s.EndDate));
            }
        }
    }
}
