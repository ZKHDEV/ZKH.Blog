using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKH.Blog.Common.Cache
{
    public static class CacheHelper
    {
        public static ICacheWriter CacheWriter { get; set; }

        static CacheHelper()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            CacheWriter = ctx.GetObject("CacheWriter") as ICacheWriter;
        }

        public static void AddCache(string key, object value)
        {
            CacheWriter.AddCache(key, value);
        }

        public static void AddCache(string key, object value, DateTime extDate)
        {
            CacheWriter.AddCache(key, value, extDate);
        }

        public static object GetCache(string key)
        {
            return CacheWriter.GetCache(key);
        }

        public static T GetCache<T>(string key)
        {
            return CacheWriter.GetCache<T>(key);
        }

        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }

        public static void SetCache(string key, object value, DateTime extDate)
        {
            CacheWriter.SetCache(key, value, extDate);
        }
    }
}
