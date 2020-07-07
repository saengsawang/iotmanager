using System;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Gateways.Entity;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;


namespace IoT.EntityFrameworkCore.Repositories
{
    public class GatewayRepository : IoTRepositoryBase<Gateway, int>, IGatewayRepository
    {
        private readonly IRepository<Device, int> _deviceRepository;
        public GatewayRepository(IRepository<Device, int> deviceRepository,IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _deviceRepository = deviceRepository;
        }

        public void AffiliateDelete(Gateway entity)
        {
            var query = _deviceRepository.GetAll().Where(d=>d.GatewayId==entity.Id);
            if (query.Any())
            {
                foreach(var device in query)
                {
                    _deviceRepository.Delete(device);
                }
            }
            Delete(entity);
        }
    }
}
