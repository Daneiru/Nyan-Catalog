namespace Core.Mapping 
{
    using AutoMapper;
    using Models;
    using DataRepository.Models;

    internal class NyanMapping 
    {
        public static void CreateMappings() {
            CreateMapDBOtoDTO();
            CreateMapDTOtoDBO();
            //CreateMapDropdown();
        }

        private static void CreateMapDTOtoDBO() {
            // Initialize mapping if none exists:
            if (Mapper.FindTypeMapFor<NyanDTO, Nyan>() == null) {
                // DTO >> DBO
                Mapper.CreateMap<NyanDTO, Nyan>()
                    .IgnoreAllPropertiesWithAnInaccessibleSetter()
                    .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
                    //.ForMember(member => member.StatusCode, opt => opt.Ignore());
            }
        }

        private static void CreateMapDBOtoDTO() {
            // Initialize mapping if none exists:
            if (Mapper.FindTypeMapFor<Nyan, NyanDTO>() == null) {
                Mapper.CreateMap<Nyan, NyanDTO>()
                    .IgnoreAllPropertiesWithAnInaccessibleSetter()
                    .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            }
        }
    }
}
