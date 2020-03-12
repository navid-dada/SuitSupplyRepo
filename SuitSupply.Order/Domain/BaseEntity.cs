using System;

namespace SuitSupply.DataAccess.Model
{
    public class BaseEntity
    { 
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}