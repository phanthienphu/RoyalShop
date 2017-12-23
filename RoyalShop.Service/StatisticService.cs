using RoyalShop.Common.ViewModels;
using RoyalShop.Data.Infrastructure;
using RoyalShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalShop.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
    }
    public class StatisticService : IStatisticService
    {
        IOrderRepository _orderRepository;
        IUnitOfWork _unitOfWork;

        public StatisticService(IOrderRepository orderRepository,IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenueStatistic(fromDate, toDate);
        }
    }
}
