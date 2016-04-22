using MassTransit;

namespace Simple.ApplicationService
{
    public interface IRequestClientCreator
    {
        IRequestClient<TRequest, TResponse> Create<TRequest, TResponse>()
            where TRequest : class
            where TResponse : class;
    }
}