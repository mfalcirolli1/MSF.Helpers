using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Mapper
{
    public static class AutoMapperExtension
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
