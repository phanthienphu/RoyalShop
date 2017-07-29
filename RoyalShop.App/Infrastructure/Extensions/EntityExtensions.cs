using RoyalShop.App.Models;
using RoyalShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoyalShop.App.Infrastructure.Extensions
{
    //Phương thức mở rộng tạo ra phương thức động cho 1 đối tượng bất kỳ
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory,PostCategoryViewModel postCategoryVM)
        {
            postCategory.CreatedDate = postCategoryVM.CreatedDate;
            postCategory.CreatedBy = postCategoryVM.CreatedBy;
            postCategory.UpdatedDate = postCategoryVM.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVM.UpdatedBy;
            postCategory.MetaKeyword = postCategoryVM.MetaKeyword;
            postCategory.MetaDescription = postCategoryVM.MetaDescription;
            postCategory.Status = postCategoryVM.Status;
        }

        public static void UpdatePostCategory(this Post post, PostViewModel postVM)
        {
            post.ID = postVM.ID;
            post.Name = postVM.Name;
            post.Description = postVM.Description;
            post.Alias = postVM.Alias;
            post.CategoryID = postVM.CategoryID;
            post.Content = postVM.Content;
            post.Image = postVM.Image;
            post.HomeFlag = postVM.HomeFlag;
            post.ViewCount = postVM.ViewCount;

            post.CreatedDate = postVM.CreatedDate;
            post.CreatedBy = postVM.CreatedBy;
            post.UpdatedDate = postVM.UpdatedDate;
            post.UpdatedBy = postVM.UpdatedBy;
            post.MetaKeyword = postVM.MetaKeyword;
            post.MetaDescription = postVM.MetaDescription;
            post.Status = postVM.Status;
        }
    }
}