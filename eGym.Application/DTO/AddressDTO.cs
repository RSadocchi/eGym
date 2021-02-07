using AutoMapper;

namespace eGym.Application.DTO
{
    public class AddressDTO
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
