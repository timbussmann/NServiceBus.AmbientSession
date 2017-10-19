using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.AmbientSession;

namespace AmbientSession.Demo
{
    public class DemoServiceB
    {
        private readonly IBusSession session;

        public DemoServiceB(IBusSession session)
        {
            this.session = session;
        }

        public Task PublishEvent()
        {
            return session.Publish(new DemoEvent(), new PublishOptions());
        }
    }
}