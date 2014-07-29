using AutoMapper;
using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Models;

namespace EdFiValidation.ApiProxy.App_Start
{
    public static class AutoMapperConfig 
    {
        public static void Config()
        {
            Mapper.CreateMap<RequestResponsePair, ApiLogModel>();
            Mapper.CreateMap<ApiRequest, ApiRequestModel>();
            Mapper.CreateMap<ApiResponse, ApiResponseModel>()
                .ForMember(dest => dest.ResponseStatusCode, map => map.MapFrom(src => (int)src.ResponseStatusCode));

            Mapper.CreateMap<UseCase, ApiUseCaseModel>();
            Mapper.CreateMap<UseCaseItem, ApiUseCaseItemModel>();
        }
    }
}