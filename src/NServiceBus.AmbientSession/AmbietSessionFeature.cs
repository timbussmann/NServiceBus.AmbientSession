using System.Threading.Tasks;
using NServiceBus.Features;

namespace NServiceBus.AmbientSession
{
    public class AmbietSessionFeature : Feature
    {
        public AmbietSessionFeature()
        {
            EnableByDefault();
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
            context.Pipeline.Register(
                new RegisterCurrentSessionBehavior(), 
                "register the current session in the ambiet bus session.");
            context.RegisterStartupTask(new RegisterSessionStartupTask());

            context.Container.RegisterSingleton(typeof(IBusSession), new CurrentSessionResolver());
        }

        private class RegisterSessionStartupTask : FeatureStartupTask
        {
            protected override Task OnStart(IMessageSession session)
            {
                BusSession.SetMessageSession(session);
                return Task.CompletedTask;
            }

            protected override Task OnStop(IMessageSession session)
            {
                return Task.CompletedTask;
            }
        }
    }
}