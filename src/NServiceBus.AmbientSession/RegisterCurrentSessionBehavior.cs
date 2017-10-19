using System;
using System.Threading.Tasks;
using NServiceBus.Pipeline;

namespace NServiceBus.AmbientSession
{
    class RegisterCurrentSessionBehavior : Behavior<IIncomingPhysicalMessageContext>
    {
        public override Task Invoke(IIncomingPhysicalMessageContext context, Func<Task> next)
        {
            BusSession.SetCurrentPipelineContext(context);

            return next();
        }
    }
}