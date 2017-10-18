using System.Threading;

namespace NServiceBus.AmbientSession
{
    public static class BusSession
    {
        private static IBusSession MessageSession = null; 
        private static readonly AsyncLocal<IBusSession> PipelineContext = new AsyncLocal<IBusSession>();

        public static IBusSession Current
        {
            get => PipelineContext.Value ?? MessageSession;
        }

        internal static void SetCurrentPipelineContext(IPipelineContext session)
        {
            PipelineContext.Value = new PipelineContextSession(session);
        }

        internal static void SetMessageSession(IMessageSession messageSession)
        {
            MessageSession = new MessageSession(messageSession);
        }
    }
}