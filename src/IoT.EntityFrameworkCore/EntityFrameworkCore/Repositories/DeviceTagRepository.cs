using System;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Devices;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class DeviceTagRepository : IoTRepositoryBase<DeviceTag, int>, IDeviceTagRepository
    {
        public DeviceTagRepository(IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
