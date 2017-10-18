namespace RoyalShop.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RoyalShop.Data.RoyalShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RoyalShop.Data.RoyalShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlide(context);
            //  This method will be called after migrating to the latest version.
            CreatePage(context);

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new RoyalShopDbContext()));
        }

        private void CreateUser(RoyalShopDbContext context)
        {
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new RoyalShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "PhanThienPhu",
            //    Email = "phanthienphu199@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "PhanThienPhu"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("phanthienphu199@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(RoyalShop.Data.RoyalShopDbContext context)
        {
            if(context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name = "Điện lạnh", Alias = "dien-lanh", Status = true },
                new ProductCategory() { Name = "Viễn thông", Alias = "vien-thong", Status = true },
                new ProductCategory() { Name = "Đồ gia dụng", Alias = "do-gia-dung", Status = true },
                new ProductCategory() { Name = "Mỹ phẩm", Alias = "my-pham", Status = true }
            };

                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges(); 
            }
        }

        private void CreateFooter(RoyalShopDbContext context)
        {
            if(context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "";
            }
        }

        private void CreateSlide(RoyalShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name = "Slide 1",
                        DisplayOrder = 1,
                        Status = true, Url = "#",
                        Image = "/Common/Client/images/bag.jpg",
                        Content =@"<h2>FLAT 50% 0FF</h2>
                                < label > FOR ALL PURCHASE < b > VALUE </ b ></ label >
                                < p > Lorem ipsum dolor sit amet,
                        consectetur adipisicing elit,
                        sed do eiusmod tempor incididunt ut labore et </ p >
                        < span class=""on-get"">GET NOW</span>"
                    },
                    new Slide() {
                        Name = "Slide 2",
                        DisplayOrder = 2,
                        Status = true,
                        Url = "#",
                        Image = "/Common/Client/images/bag1.jpg",
                        Content=@"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>					
								<span class=""on-get"">GET NOW</span>"
                    }
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(RoyalShopDbContext context)
        {
            if(context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "Giới thiệu",
                    Alias = "gioi-thieu",
                    Content = @"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident,
                    similique sunt in culpa qui officia deserunt mollitia animi,
                    id est laborum et dolorum fuga.Et harum quidem rerum facilis est et expedita distinctio.Nam libero tempore,
                    cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus,
                    omnis voluptas assumenda est,
                    omnis dolor repellendus.Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae.Itaque earum rerum hic tenetur a sapiente delectus,
                    ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.",
                    Status = true
                };

                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
    }
}
