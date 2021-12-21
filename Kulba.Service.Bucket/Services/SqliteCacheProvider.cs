using System;

namespace Kulba.Service.Bucket.Services
{
    public class SqliteCacheProvider : ICacheProvider
    {
        public void ClearCache(string key)
        {
            throw new NotImplementedException();
        }

        public T GetFromCache<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetCache<T>(string key, T value) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
            throw new NotImplementedException();
        }
    }
}