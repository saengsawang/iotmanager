using System;
using Abp.Domain.Repositories;

namespace IoT.Core.Gateways
{
    public interface IGatewayRepository : IRepository<Gateway, int>
    {
        void AffiliateDelete(Gateway entity);
    }
}
