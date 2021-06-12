using System;

namespace Shared.Events
{
    public interface SubmitOrder
    {
        Guid OrderId { get; }
    }
}