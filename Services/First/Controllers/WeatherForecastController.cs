using System;
using System.Threading.Tasks;
using First.Subscribers;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace First.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IPublishEndpoint _endpoint;

        public WeatherForecastController(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        [HttpGet]
        public string Get() => "first service";

        [HttpPost("{price}")]
        public async Task<string> Post(double price)
        {
            await _endpoint.Publish(new PaymentAdded(price));
            
            return "done";
        }

        //
        IMediator mediator = Bus.Factory.CreateMediator(cfg =>
        {
            // SubmitOrder, GetOrderStatus
            cfg.Consumer<SubmitOrderConsumer>();
            // cfg.Consumer<OrderStatusConsumer>();
            // SubmitOrder
        });

        [HttpPut]
        public async Task<string> Put()
        {
            Guid orderId = NewId.NextGuid();
            await mediator.Send<SubmitOrder>(new { OrderId = orderId });

            var client = mediator.CreateRequestClient<GetOrderStatus>();

            var response = await client.GetResponse<OrderStatus>(new { OrderId = orderId });

            Console.WriteLine("Order Status: {0}", response.Message.Status);
            return "done";
        }
    }
}
