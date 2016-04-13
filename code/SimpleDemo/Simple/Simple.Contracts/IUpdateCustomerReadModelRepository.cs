﻿using Simple.CommandStack.Events;

namespace Simple.Contracts
{
    public interface IUpdateCustomerReadModelRepository
    {
        void Update(ICustomerCreatedEvent command);
    }
}