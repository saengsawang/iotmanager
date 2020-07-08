using System;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Gateways;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections;
using IoT.Core.Devices;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class GatewayRepository : IoTRepositoryBase<Gateway, int>, IGatewayRepository
    {
        private readonly IDeviceManager _deviceManager;
        public GatewayRepository(IDeviceManager deviceManager, IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _deviceManager = deviceManager;
        }

        public void AffiliateDelete(Gateway entity)
        {
            var query = _deviceManager.GetAll().Where(d=>d.GatewayId==entity.Id);
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
                _deviceManager.Delete((Device)device);
            }
            Delete(entity);
        }
    }
}
