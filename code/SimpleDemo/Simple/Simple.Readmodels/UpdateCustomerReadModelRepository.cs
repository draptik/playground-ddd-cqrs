﻿using System.Linq;
using Simple.CommandStack.Events;
using Simple.Contracts;

namespace Simple.Readmodels
{
    public class UpdateCustomerReadModelRepository : IUpdateCustomerReadModelRepository
    {
        private readonly CustomerContext _context;

        public UpdateCustomerReadModelRepository()
        {
            _context = new CustomerContext();
        }

        public void Update(ICustomerCreatedEvent command)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id.Equals(command.Id));

            if (customer != null)
            {
                customer.Name = command.Name;
                customer.Address = command.Address;
            }
            else
            {
                _context.Customers.Add(new Customer { Id = command.Id, Name = command.Name, Address = command.Address });
            }

            _context.SaveChanges();
        }
    }
}