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
        private readonly IBusSession session;

        public DemoMessageHandler(DemoServiceA demoServiceA, DemoServiceB demoServiceB, IBusSession session)
        {
            this.demoServiceA = demoServiceA;
            this.demoServiceB = demoServiceB;
            this.session = session;
        }

        public async Task Handle(DemoMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Received {nameof(DemoMessage)}");

            await session.Publish(new DemoEvent());
            await demoServiceA.PublishEvent();
            await demoServiceB.PublishEvent();
        }
    }
}