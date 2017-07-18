using RoyalShop.Data.Infrastructure;
using RoyalShop.Model.Models;

namespace RoyalShop.Data.Repositories
{
    public interface IFooterRepository : IRepository<Footer> //khai báo các phương thức không nằm trong RepositoryBase
    {
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}