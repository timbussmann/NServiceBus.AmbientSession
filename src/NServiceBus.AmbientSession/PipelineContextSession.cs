using System;
using System.Threading.Tasks;

namespace NServiceBus.AmbientSession
{
    class PipelineContextSession : IBusSession
    {
        private readonly IPipelineContext pipelineContext;

        public PipelineContextSession(IPipelineContext pipelineContext)
        {
            this.pipelineContext = pipelineContext;
        }

        public Task Send(object message, SendOptions options)
        {
            return pipelineContext.Send(message, options);
        }

        public Task Send<T>(Action<T> messageConstructor, SendOptions options)
        {
            return pipelineContext.Send(messageConstructor, options);
        }

        public Task Publish(object message, PublishOptions options)
        {
            return pipelineContext.Publish(message, options);
        }

        public Task Publish<T>(Action<T> messageConstructor, PublishOptions publishOptions)
        {
            return pipelineContext.Publish(messageConstructor, publishOptions);
        }
    }
}