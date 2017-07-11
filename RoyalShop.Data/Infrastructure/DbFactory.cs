using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalShop.Data.Infrastructure
{
    public class DbFactory : Disposable,IDbFactory
    {
        RoyalShopDbContext dbContext;

        public RoyalShopDbContext Init()
        {
            return dbContext ?? (dbContext = new RoyalShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
