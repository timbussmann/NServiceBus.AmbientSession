using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.AmbientSession;

namespace AmbientSession.Demo
{
    public class DemoServiceA
    {
        private IBusSession busSession;

        public DemoServiceA(IBusSession session)
        {
            busSession = session;
        }

        public Task PublishEvent()
        {
            return busSession.Publish(new DemoEvent(), new PublishOptions());
        }
    }
}