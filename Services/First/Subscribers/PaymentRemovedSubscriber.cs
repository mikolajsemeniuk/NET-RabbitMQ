using System;
using System.Threading.Tasks;
using MassTransit;
using Shared.Events;

namespace First.Subscribers
{
    public class PaymentRemovedSubscriber : IConsumer<PaymentRemoved>
    {
        public Task Consume(ConsumeContext<PaymentRemoved> context)
        {
            Console.WriteLine($"from first: {context.Message.AccountId}");
            return Task.CompletedTask;
        }
    }
}