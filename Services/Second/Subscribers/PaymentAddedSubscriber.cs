using System;
using System.Threading.Tasks;
using MassTransit;
using Shared.Events;

namespace Second.Subscribers
{
    public class PaymentAddedSubscriber : IConsumer<PaymentAdded>
    {
        private readonly IPublishEndpoint _endpoint;

        public PaymentAddedSubscriber(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task Consume(ConsumeContext<PaymentAdded> context)
        {
            Console.WriteLine($"price: {context.Message.Price}");
            
            // if we use throw rabbitmq create {endpointname_error}
            // and will stay in queue until someone subscribe to them
            // throw new Exception("check");

            // if (context.Message.Price == 20.0)                
            //     await _endpoint.Publish(new PaymentRemoved(3));
            await context.RespondAsync<PaymentRemoved>(new PaymentRemoved(3));
        }
    }
}