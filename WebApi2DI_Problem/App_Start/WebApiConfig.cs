using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using WebApi2DI_Problem.Controllers;
using WebApi2DI_Problem.Models;

namespace WebApi2DI_Problem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            var services = new ServiceCollection();

            services.AddTransient<ValuesController>();
            services.AddScoped<ITest, Test>();

            var provider = services.BuildServiceProvider();

            config.DependencyResolver = new MyDependencyResolver(provider);

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public class MyDependencyResolver : IDependencyResolver
        {
            private readonly ServiceProvider _provider;

            public MyDependencyResolver(ServiceProvider provider)
            {
                _provider = provider;
            }

            public void Dispose()
            {
                _provider.Dispose(); // 目前測試無法重複使用 (待解決)
            }

            public object GetService(Type serviceType)
            {
                return _provider.GetService(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return _provider.GetServices(serviceType);
            }

            public IDependencyScope BeginScope()
            {
                return this;
            }
        }
    }
}
