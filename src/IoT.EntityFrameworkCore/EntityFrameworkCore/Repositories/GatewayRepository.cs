using System;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Gateways.Entity;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections;

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
            ArrayList list = new ArrayList(query.Count());
            if (query.Any())
            {
                foreach (var device in query)
                {
                    list.Add((Device)device);
                }
            }
            foreach (var device in list)
            {
                _deviceRepository.Delete((Device)device);
            }
            Delete(entity);
        }
    }
}
