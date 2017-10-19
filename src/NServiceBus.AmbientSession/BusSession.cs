using System;
using System.Threading;

namespace NServiceBus.AmbientSession
{
    public static class BusSession
    {
        private static IBusSession MessageSession = null;
        private static readonly AsyncLocal<IBusSession> PipelineContext = new AsyncLocal<IBusSession>();

        public static IBusSession Current => PipelineContext.Value ?? MessageSession;

        internal static void SetCurrentPipelineContext(IPipelineContext session)
        {
            if (PipelineContext.Value != null)
            {
                throw new InvalidOperationException("Attempt to overwrite an existing pipeline context in BusSession.Current.");
            }

            PipelineContext.Value = new PipelineContextSession(session);
        }

        internal static void SetMessageSession(IMessageSession messageSession)
        {
            if (MessageSession != null)
            {
                throw new InvalidOperationException("Attempt to overwrite an existing message session in BusSession.Current.");
            }

            MessageSession = new MessageSession(messageSession);
        }
    }
}