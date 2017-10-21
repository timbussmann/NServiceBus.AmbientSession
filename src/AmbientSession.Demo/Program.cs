using System;
using System.Threading.Tasks;
using Autofac;
using NServiceBus;
using NServiceBus.AmbientSession;

namespace AmbientSession.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            var endpointConfig = new EndpointConfiguration("AmbientSession.Demo");

            var transport = endpointConfig.UseTransport<LearningTransport>();
            transport.Routing().RouteToEndpoint(typeof(DemoMessage), "AmbientSession.Demo");

            endpointConfig.RegisterComponents(c => c.ConfigureComponent(typeof(DemoServiceA), DependencyLifecycle.SingleInstance));

            // use services resolving IBusSession as dependency
            endpointConfig.RegisterComponents(c => c.ConfigureComponent(typeof(DemoServiceB), DependencyLifecycle.InstancePerUnitOfWork));
            endpointConfig.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));

            await Endpoint.Start(endpointConfig);

            Console.WriteLine("Press any key to send a message, press [esc] to exit.");
            var session = container.Resolve<IBusSession>();
            var demoServiceA = container.Resolve<DemoServiceA>();
            while(true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                await demoServiceA.PublishEvent();

                await session.Send(new DemoMessage());
            }
        }
    }
}
