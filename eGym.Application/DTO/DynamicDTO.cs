using AutoMapper;

namespace eGym.Application.DTO
{
    public class DynamicDTO
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
