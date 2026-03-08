using OrderServiceApi.Api.Data.Dtos.Requests;

namespace OrderServiceApi.Api.Service.Interface
{
    public interface IEventPublisher
    {
        Task PublishAsync(NotificationEvent evt, CancellationToken ct=default);
    }
}