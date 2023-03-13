using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSF.Util.Cache
{
    // Caching is used with Values that are repeatedly used in the application, and these values won’t change often

    public class Caching
    {
        public const string CacheKey = nameof(Caching);
        private readonly IMemoryCache _cache;
        private readonly List<CacheExempleModel> _repository;

        public Caching(IMemoryCache cache, List<CacheExempleModel> repository)
        {
            _cache = cache;
            _repository = repository;
        }


        // Revisar
        public IReadOnlyList<CacheExempleModel> GetAll()
        {
            if (!_cache.TryGetValue(CacheKey, out IReadOnlyList<CacheExempleModel> result))
            {
                var departments = _repository;
                result = departments.ToList();
                var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));
                _cache.Set(CacheKey, result, options);
            }

            return result;
        }

        public void RemoveAll()
        {
            _cache.Remove(CacheKey);
        }
    }


    public class CacheExempleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
