using System;

namespace Shared.Events
{
    public interface OrderStatus
    {
        Guid OrderId { get; }
        string Status { get; }
    }
}