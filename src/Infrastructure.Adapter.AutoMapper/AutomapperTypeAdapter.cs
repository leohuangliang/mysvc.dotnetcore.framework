using AutoMapper;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;

namespace MySvc.DotNetCore.Framework.Infrastructure.Adapter.AutoMapper
{
    /// <summary>
    /// 基于AutoMapper实现的类型转换
    /// </summary>
    public class AutomapperTypeAdapter : ITypeAdapter
    {
        private readonly IMapper _mapper;

        public AutomapperTypeAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget Adapt<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            return _mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return _mapper.Map<TTarget>(source);
        }
    }
}
