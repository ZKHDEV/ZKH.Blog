using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKH.Blog.Common.Cache
{
    public class MemcachedWriter : ICacheWriter
    {
        private MemcachedClient memcachedClient;
        public MemcachedWriter()
        {
            string[] servers = ConfigurationManager.AppSettings["MemcachedServerList"].Split(',');

            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();

            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;

            memcachedClient = mc;
        }

        public void AddCache(string key, object value)
        {
            memcachedClient.Add(key, value);
        }

        public void AddCache(string key, object value, DateTime extDate)
        {
            memcachedClient.Add(key, value, extDate);
        }

        public object GetCache(string key)
        {
            return memcachedClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)memcachedClient.Get(key);
        }

        public void SetCache(string key, object value)
        {
            memcachedClient.Set(key, value);
        }

        public void SetCache(string key, object value, DateTime extDate)
        {
            memcachedClient.Set(key, value, extDate);
        }
    }
}
