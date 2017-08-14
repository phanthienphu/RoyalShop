﻿namespace RoyalShop.Data.Migrations
{
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
            //  This method will be called after migrating to the latest version.

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new RoyalShopDbContext()));

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
    }
}
