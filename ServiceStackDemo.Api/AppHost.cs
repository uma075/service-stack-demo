using ServiceStackDemo.Api.Services;
using System;
using ServiceStack;
using Funq;
using ServiceStack.Redis;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStackDemo.Api.Dto;
using ServiceStack.Api.Swagger;
using ServiceStack.Api.OpenApi;

namespace ServiceStackDemo.Api
{
    // Create your ServiceStack Web Service with a singleton AppHost
    public class AppSelfHost : AppSelfHostBase
    {
        // Initializes your AppHost Instance, with the Service Name and assembly containing the Services
        public AppSelfHost() : base("Service Stack Demo", typeof(CustomerService).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            // Add swagger support to API
            Plugins.Add(new SwaggerFeature());

            // Inline database
            container.Register<IDbConnectionFactory>(c =>
             new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.CreateTableIfNotExists<Customer>();
            }           
        }
    }

    public class AppHost : AppHostBase
    {
        // Initializes your AppHost Instance, with the Service Name and assembly containing the Services
        public AppHost() : base("Service Stack Demo", typeof(CustomerService).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {        
         
            // Inline database
            container.Register<IDbConnectionFactory>(c =>
             new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.CreateTableIfNotExists<Customer>();
            }

            // Add swagger support to API
            Plugins.Add(new SwaggerFeature());
        }
    }

}
