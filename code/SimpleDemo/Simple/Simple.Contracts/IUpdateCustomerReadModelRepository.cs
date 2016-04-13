using Simple.CommandStack.Commands;

namespace Simple.Contracts
{
    public interface IUpdateCustomerReadModelRepository
    {
        void Update(IUpdateViewModelCommand command);
    }
}