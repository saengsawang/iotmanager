using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Gateways
{
    public class GatewayManager : DomainService, IGatewayManager
    {
        private readonly IGatewayRepository _gatewayRepositories;
        public GatewayManager(IGatewayRepository gatewayRepositories)
        {
            _gatewayRepositories = gatewayRepositories;
        }

        public void Delete(Gateway entity)
        {
            _gatewayRepositories.AffiliateDelete(entity);
        }

        public Gateway Update(Gateway entity)
        {
            return _gatewayRepositories.Update(entity);
        }

        public IQueryable<Gateway> GetAll()
        {
            return _gatewayRepositories.GetAll();
        }
    }
}
