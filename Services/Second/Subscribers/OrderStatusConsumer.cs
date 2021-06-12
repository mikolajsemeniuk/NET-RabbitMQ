using System;
using System.Threading.Tasks;
using MassTransit;
using Shared.Events;

namespace Second.Subscribers
{
    public class OrderStatusConsumer : IConsumer<GetOrderStatus>
    {
        public async Task Consume(ConsumeContext<GetOrderStatus> context)
        {
            await context.RespondAsync<OrderStatus>(new 
                { 
                    context.Message.OrderId, 
                    Status = "Pending" 
                });
        }
    }
}