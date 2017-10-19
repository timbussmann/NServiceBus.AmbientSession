using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.AmbientSession;

namespace AmbientSession.Demo
{
    public class DemoMessageHandler : IHandleMessages<DemoMessage>
    {
        private readonly DemoServiceA demoServiceA;
        private readonly DemoServiceB demoServiceB;

        public DemoMessageHandler(DemoServiceA demoServiceA, DemoServiceB demoServiceB)
        {
            this.demoServiceA = demoServiceA;
            this.demoServiceB = demoServiceB;
        }

        public async Task Handle(DemoMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Received {nameof(DemoMessage)}");

            await demoServiceA.PublishEvent();
            await demoServiceB.PublishEvent();
        }
    }
}