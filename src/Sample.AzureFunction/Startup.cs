using System;
using Autofac;
using Autofac.Extensions.DependencyInjection.AzureFunctions;
using MassTransit;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sample.AzureFunction;

[assembly: FunctionsStartup(typeof(Startup))]


namespace Sample.AzureFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Uncomment this block and it will crash because MassTransitOptions has not registered.
            //builder.Services
            //    .AddMassTransit(x =>
            //    {
            //        x.UsingRabbitMq((context, cfg) =>
            //        {

            //            var options = context.GetRequiredService<MassTransitOptions>();
            //            cfg.Host(new Uri(options.Url));
            //        });
            //    })
            //    .AddSingleton<IAsyncBusHandle, AsyncBusHandle>()
            //    .RemoveMassTransitHostedService();
            //Comment the above out and call the '/api/option' endpoint and you'll see that MassTransitOptions has been registered
            builder.UseAutofacServiceProviderFactory(ConfigureContainer);
        }

        private void ConfigureContainer(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsSelf() // Azure Functions core code resolves a function class by itself.
                .InNamespace("Sample.AzureFunction")
                .InstancePerTriggerRequest(); // This will scope nested dependencies to each function execution

            builder.Register(_ => new MassTransitOptions
            {
                Url = "rabbitmq://localhost/dev",
            });
        }
    }
}