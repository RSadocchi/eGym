using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Core.Domain;
using System;

namespace eGym.Application.DTO
{
    public class AnagDTO
    {

        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;
            }
        }
    }
}
