using System;
using System.Threading.Tasks;

namespace NServiceBus.AmbientSession
{
    class CurrentSessionResolver : IBusSession
    {
        public Task Send(object message, SendOptions options)
        {
            return BusSession.Current.Send(message, options);
        }

        public Task Send<T>(Action<T> messageConstructor, SendOptions options)
        {
            return BusSession.Current.Send(messageConstructor, options);
        }

        public Task Publish(object message, PublishOptions options)
        {
            return BusSession.Current.Publish(message, options);
        }

        public Task Publish<T>(Action<T> messageConstructor, PublishOptions publishOptions)
        {
            return BusSession.Current.Publish(messageConstructor, publishOptions);
        }
    }
}