using RoyalShop.App.Models;
using RoyalShop.Model.Models;
using RoyalShopp.App.Models;
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

        public static void UpdateProductCategory(this ProductCategory  productCategory, ProductCategoryViewModel productCategoryVM)
        {
            productCategory.ID = productCategoryVM.ID;
            productCategory.Name = productCategoryVM.Name;
            productCategory.Description = productCategoryVM.Description;
            productCategory.Alias = productCategoryVM.Alias;
            productCategory.ParentID = productCategoryVM.ParentID;
            productCategory.DisplayOrder = productCategoryVM.DisplayOrder;
            productCategory.Image = productCategoryVM.Image;
            productCategory.HomeFlag = productCategoryVM.HomeFlag;

            productCategory.CreatedDate = productCategoryVM.CreatedDate;
            productCategory.CreatedBy = productCategoryVM.CreatedBy;
            productCategory.UpdatedDate = productCategoryVM.UpdatedDate;
            productCategory.UpdatedBy = productCategoryVM.UpdatedBy;
            productCategory.MetaKeyword = productCategoryVM.MetaKeyword;
            productCategory.MetaDescription = productCategoryVM.MetaDescription;
            productCategory.Status = productCategoryVM.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Description = postVm.Description;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.Content = postVm.Content;
            post.Image = postVm.Image;
            post.HomeFlag = postVm.HomeFlag;
            post.ViewCount = postVm.ViewCount;

            post.CreatedDate = postVm.CreatedDate;
            post.CreatedBy = postVm.CreatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.Name = productVm.Name;
            product.Description = productVm.Description;
            product.Alias = productVm.Alias;
            product.CategoryID = productVm.CategoryID;
            product.Content = productVm.Content;
            product.Image = productVm.Image;
            product.MoreImages = productVm.MoreImages;
            product.Price = productVm.Price;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Warranty = productVm.Warranty;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.ViewCount = productVm.ViewCount;

            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.MetaKeyword = productVm.MetaKeyword;
            product.MetaDescription = productVm.MetaDescription;
            product.Status = productVm.Status;
            product.Tags = productVm.Tags;
            product.Quantity = productVm.Quantity;
            product.OriginalPrice = productVm.OriginalPrice;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVM)
        {
            feedback.Name = feedbackVM.Name;
            feedback.Message = feedbackVM.Message;
            feedback.Email = feedbackVM.Email;
            feedback.CreatedDate = DateTime.Now;
            feedback.Status = feedbackVM.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVM)
        {
            order.CustomerName = orderVM.CustomerName;
            order.CustomerAddress = orderVM.CustomerAddress;
            order.CustomerEmail = orderVM.CustomerEmail;
            order.CustomerMobile = orderVM.CustomerMobile;
            order.CustomerMessage = orderVM.CustomerMessage;
            order.PaymentMethod = orderVM.PaymentMethod;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderVM.CreatedBy;
            order.Status = orderVM.Status;
            order.CustomerId = orderVM.CustomerId;
            //order.OrderDetails = AutoMapper.Mapper.Map<IEnumerable<OrderDetailViewModel>, IEnumerable<OrderDetail>>(orderVM.OrderDetails);
        }
    }
}