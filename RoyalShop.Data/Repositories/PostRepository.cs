﻿using System;
using System.Collections;
using System.Collections.Generic;
using RoyalShop.Data.Infrastructure;
using RoyalShop.Model.Models;
using System.Linq;

namespace RoyalShop.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            //kết quả trả về là IQueryable
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tag && p.Status
                        orderby p.CreatedDate descending
                        select p;

            totalRow = query.Count();//đếm số dòng

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize); //lấy từ đâu đến đâu

            return query;
        }
    }
}