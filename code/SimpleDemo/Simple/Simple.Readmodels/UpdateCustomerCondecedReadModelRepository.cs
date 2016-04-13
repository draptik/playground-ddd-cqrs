using System.Linq;
using Simple.CommandStack.Events;
using Simple.Contracts;

namespace Simple.Readmodels
{
    public class UpdateCustomerCondecedReadModelRepository : IUpdateCustomerCondencedRepository
    {
        private readonly CustomerContext _context;

        public UpdateCustomerCondecedReadModelRepository()
        {
            _context = new CustomerContext();
        }

        public void Update(ICustomerCreatedEvent command)
        {
            var customer = _context.CustomerForLists.SingleOrDefault(x => x.Id.Equals(command.Id));

            if (customer != null)
            {
                customer.Name = command.Name;
            }
            else
            {
                _context.CustomerForLists.Add(new CustomerForList { Id = command.Id, Name = command.Name });
            }

            _context.SaveChanges();
        }
    }
}