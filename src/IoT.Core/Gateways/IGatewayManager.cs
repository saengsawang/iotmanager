using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Gateways
{
    public interface IGatewayManager : IDomainService
    {
        void Delete(Gateway entity);
        public IQueryable<Gateway> GetAll();
    }
}
