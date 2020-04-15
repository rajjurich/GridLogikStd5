using Autofac;
using Autofac.Integration.WebApi;
using Domain.Core;
using Domain.Entities;
using Domain.Services;
using GridLogik.API.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GridLogik.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);




            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            var apiAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterApiControllers(apiAssembly);

            builder.RegisterType<etools_devEntities>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            

            builder.RegisterGeneric(typeof(EntityRepository<>))
                   .As(typeof(IEntityRepository<>))
                   .InstancePerLifetimeScope();

            var referencedAssemblyName = apiAssembly.GetReferencedAssemblies()
                                                   .Where(n => n.Name.Equals("Domain"))
                                                   .FirstOrDefault();
            Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);

            builder.RegisterAssemblyTypes(referencedAssembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces().PropertiesAutowired();
            
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
