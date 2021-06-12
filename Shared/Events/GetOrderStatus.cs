using System;

namespace Shared.Events
{
    public interface GetOrderStatus
    {
        Guid OrderId { get; }
    }
}