using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MessageSenderModels;

namespace UrFUCoworkingsAPIGateway.Rabbit.Services
{
    public class MessageProvider
    {
        private readonly IServiceProvider Provider;
        public MessageProvider(IServiceProvider provider) => Provider = provider;

        public Task SendMessage(Message message)
        {
            using IServiceScope scope = Provider.CreateScope();
            Bind<IMessageBus, IPublishEndpoint> publishEndpoint = scope.ServiceProvider.GetRequiredService<Bind<IMessageBus, IPublishEndpoint>>();
            publishEndpoint.Value.Publish(message);
            return Task.CompletedTask;
        }
    }
}
