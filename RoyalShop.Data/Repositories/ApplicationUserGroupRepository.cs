using RoyalShop.Data.Infrastructure;
using RoyalShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalShop.Data.Repositories
{
    public interface IApplicationUserGroupRepository : IRepository<ApplicationUserGroup>
    {

    }
    class ApplicationUserGroupRepository : RepositoryBase<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        public ApplicationUserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
