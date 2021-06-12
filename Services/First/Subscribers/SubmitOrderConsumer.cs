using System;
using System.Threading.Tasks;
using MassTransit;
using Shared.Events;

namespace First.Subscribers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public Task Consume(ConsumeContext<SubmitOrder> context)
        {
            Console.WriteLine($"message: {context.Message.OrderId}");
            return Task.CompletedTask;
        }
    }
}