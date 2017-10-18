using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.AmbientSession;

namespace AmbientSession.Demo
{
    public class DemoServiceA
    {
        public Task PublishEvent()
        {
            return BusSession.Current.Publish(new DemoEvent(), new PublishOptions());
        }
    }
}