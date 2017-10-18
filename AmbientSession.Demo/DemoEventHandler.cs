using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.AmbientSession;

namespace AmbientSession.Demo
{
    public class DemoEventHandler : IHandleMessages<DemoEvent>
    {
        public Task Handle(DemoEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Received {nameof(DemoEvent)}");
            return Task.CompletedTask;
        }
    }
}