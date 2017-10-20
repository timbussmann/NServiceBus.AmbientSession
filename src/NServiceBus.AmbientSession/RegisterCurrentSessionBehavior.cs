using System;
using System.Threading.Tasks;
using NServiceBus.Pipeline;

namespace NServiceBus.AmbientSession
{
    class RegisterCurrentSessionBehavior : Behavior<IIncomingPhysicalMessageContext>
    {
        public override async Task Invoke(IIncomingPhysicalMessageContext context, Func<Task> next)
        {
            using (var session = new PipelineContextSession(context))
            {
                BusSession.SetCurrentPipelineContext(session);

                await next();
            }
        }
    }
}