using Showmatics.Shared.Events;

namespace Showmatics.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}