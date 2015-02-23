using System.Collections.Generic;
using System.Linq;
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

            Mapper.CreateMap<ApiTransaction, ApiTransactionModel>()
                .ForMember(dest => dest.Headers, 
                            map => map.MapFrom(src => 
                                src.Headers.Select(h => new KeyValuePair<string, string>(h.Key, h.Value))
                    ));

            Mapper.CreateMap<UseCase, ApiUseCaseModel>();
            Mapper.CreateMap<UseCaseItem, ApiUseCaseItemModel>();
            Mapper.CreateMap<ValidationUseCase, ValidationCaseModel>()
                  .ForMember(dest => dest.UseCaseId, map => map.MapFrom(src => src.Id));
            Mapper.CreateMap<ValidationUseCaseItem, ValidationCaseItemModel>();
        }
    }
}