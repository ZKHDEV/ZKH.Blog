using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ZKH.Blog.Model;

namespace ZKH.Blog.DAL
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            DbContext db = CallContext.GetData("DbContext") as DbContext;

            if (db == null)
            {
                db = new DataModelContainer();
                CallContext.SetData("DbContext", db);
            }

            return db;
        }
    }
}
