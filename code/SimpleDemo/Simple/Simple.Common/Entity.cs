using System;

namespace Simple.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}