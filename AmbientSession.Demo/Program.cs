using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.AmbientSession;

namespace AmbientSession.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointConfig = new EndpointConfiguration("AmbientSession.Demo");

            endpointConfig.UseTransport<LearningTransport>();

            var demoServiceA = new DemoServiceA();
            endpointConfig.RegisterComponents(c => c.RegisterSingleton(demoServiceA));

            // register IBusSession via DI:
            endpointConfig.RegisterComponents(c => c.ConfigureComponent(
                b => BusSession.Current, DependencyLifecycle.InstancePerCall));

            // use services resolving IBusSession as dependency
            endpointConfig.RegisterComponents(c => c.ConfigureComponent(typeof(DemoServiceB), DependencyLifecycle.InstancePerUnitOfWork));

            var endpoint = await Endpoint.Start(endpointConfig);


            Console.WriteLine("Press any key to send a message, press [esc] to exit.");
            do
            {
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                var sendOptions = new SendOptions();
                sendOptions.RouteToThisEndpoint();

                await demoServiceA.PublishEvent();

                await BusSession.Current.Send(new DemoMessage(), sendOptions);
            } while (true);

        }
    }
}
