using System;
using Abp.Domain.Repositories;

namespace IoT.Core.Fields
{
    public interface IFieldRepository : IRepository<Field, int>
    {
        void AffiliateDelete(Field entity);
    }
}
