using AutoMapper;

namespace eGym.Application.DTO
{
    public class AthleteDTO
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
