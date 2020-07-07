using System;
using Abp.Domain.Repositories;

namespace IoT.Core.Factories.Entity
{
    public interface IFactoryRepository : IRepository<Factory, int>
    {
        void AffiliateDelete(Factory entity);
    }
}
