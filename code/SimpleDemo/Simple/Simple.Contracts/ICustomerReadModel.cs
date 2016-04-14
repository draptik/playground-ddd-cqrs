using System;
using System.Collections.Generic;
using Simple.CommandStack.Events;
using Simple.Domain.QueryModels;

namespace Simple.Contracts
{
    public interface ICustomerReadModel
    {
        void Update(ICustomerCreatedEvent command);
        void Update(ICustomerAddressChangedEvent command);
        CustomerDetails FindById(Guid id);
        IEnumerable<CustomerForList> GetAll();
    }
}