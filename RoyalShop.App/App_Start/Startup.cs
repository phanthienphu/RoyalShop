using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using RoyalShop.Data.Infrastructure;
using RoyalShop.Data;
using RoyalShop.Data.Repositories;
using RoyalShop.Service;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Http;

[assembly: OwinStartup(typeof(RoyalShop.App.App_Start.Startup))]

namespace RoyalShop.App.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);
        }

        //cấu hình
        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            //tự khởi tạo các phương thức mà ko cần tạo trước
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<RoyalShopDbContext>().AsSelf().InstancePerRequest();

            //lấy ra các file có hậu tố Repository và Service
            // Repositories
            builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            Autofac.IContainer container = builder.Build(); //gán tất cả các khai báo trên vào 1 container ( thùng chứa )
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//thay thế cơ chế mặc định bằng cơ chế đã register ở trên

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver (web api và controller đều có thể dùng chung)
        }
    }
}
