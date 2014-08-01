using EdFiValidation.ApiProxy.Core.Models;
using EdFiValidation.ApiProxy.Models;

namespace EdFiValidation.ApiProxy.Helpers
{
    public interface IUiModelMapper
    {
        T Mapper<T, TM>(T model)
            where T : ModelBase
            where TM : UiModelBase;
    }
    public class UiModelMapper : IUiModelMapper
    {
        public T Mapper<T, TM>(T model)
            where T : ModelBase
            where TM : UiModelBase
        {
            return AutoMapper.Mapper.Map<T>(model);
        }
    }
}