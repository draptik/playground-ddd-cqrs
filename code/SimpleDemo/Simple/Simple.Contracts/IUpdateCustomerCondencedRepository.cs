using Simple.CommandStack.Events;

namespace Simple.Contracts
{
    public interface IUpdateCustomerCondencedRepository
    {
        void Update(ICustomerCreatedEvent command);
    }
}