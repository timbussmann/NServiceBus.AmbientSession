using System;
using System.Threading.Tasks;

namespace NServiceBus.AmbientSession
{
    public class MessageSession : IBusSession
    {
        private readonly IMessageSession messageSession;

        public MessageSession(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }

        public Task Send(object message, SendOptions options)
        {
            return messageSession.Send(message, options);
        }

        public Task Send<T>(Action<T> messageConstructor, SendOptions options)
        {
            return messageSession.Send(messageConstructor, options);
        }

        public Task Publish(object message, PublishOptions options)
        {
            return messageSession.Publish(message, options);
        }

        public Task Publish<T>(Action<T> messageConstructor, PublishOptions publishOptions)
        {
            return messageSession.Publish(messageConstructor, publishOptions);
        }
    }
}