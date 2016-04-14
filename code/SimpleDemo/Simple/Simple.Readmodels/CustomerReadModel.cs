using System;
using System.Collections.Generic;
using System.Linq;
using Simple.CommandStack.Events;
using Simple.Contracts;
using Simple.Domain.QueryModels;

namespace Simple.Readmodels
{
    public class CustomerReadModel : ICustomerReadModel
    {
        private readonly CustomerContext _context;

        public CustomerReadModel()
        {
            _context = new CustomerContext();
        }

        public void Update(ICustomerCreatedEvent command)
        {
            var customer = _context.CustomerDetails.SingleOrDefault(x => x.Id.Equals(command.Id));

            if (customer != null)
            {
                // should never happen
                customer.Name = command.Name;
                customer.Address = command.Address;
            }
            else
            {
                _context.CustomerDetails.Add(new CustomerDetails { Id = command.Id, Name = command.Name, Address = command.Address });
            }

            _context.SaveChanges();
        }

        public void Update(ICustomerAddressChangedEvent command)
        {
            var customer = _context.CustomerDetails.SingleOrDefault(x => x.Id.Equals(command.Id));
            if (customer != null)
            {
                customer.Address = command.Address;
                _context.SaveChanges();
            }
        }

        public CustomerDetails FindById(Guid id)
        {
            // TODO error handling
            var result =  _context.CustomerDetails.SingleOrDefault(x => x.Id.Equals(id));
            return result;
        }

        public IEnumerable<CustomerForList> GetAll()
        {
            return _context.CustomerForLists;
        }
    }
}