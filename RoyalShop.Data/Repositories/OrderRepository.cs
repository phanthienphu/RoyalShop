﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using RoyalShop.Data.Infrastructure;
using RoyalShop.Model.Models;

namespace RoyalShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        //IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        //public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        //{
        //    var parameters = new SqlParameter[]{
        //        new SqlParameter("@fromDate",fromDate),
        //        new SqlParameter("@toDate",toDate)
        //    };
        //    return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate,@toDate", parameters);
        //}
    }
}