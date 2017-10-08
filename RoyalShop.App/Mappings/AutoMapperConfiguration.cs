using AutoMapper;
using RoyalShop.App.Models;
using RoyalShop.Model.Models;

namespace RoyalShop.App.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //dùng cho phiên bản 4.5 trở xuống
            /*Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();*/

            //dùng cho phiên bản 5.0 trở lên
            /*Mapper.Initialize(cfg => cfg.CreateMap<Post, PostViewModel>());*/

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductTag, ProductTagViewModel>();
                cfg.CreateMap<Footer, FooterViewModel>();
                cfg.CreateMap<Slide, SlideViewModel>();
            });
        }
    }
}