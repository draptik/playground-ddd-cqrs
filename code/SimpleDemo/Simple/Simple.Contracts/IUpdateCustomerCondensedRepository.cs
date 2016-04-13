using Simple.CommandStack.Events;

namespace Simple.Contracts
{
    public interface IUpdateCustomerCondensedRepository
    {
        void Update(ICustomerCreatedEvent command);
    }
}