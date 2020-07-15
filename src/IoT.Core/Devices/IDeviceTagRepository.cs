using System;
using Abp.Domain.Repositories;

namespace IoT.Core.Devices
{
    public interface IDeviceTagRepository : IRepository<DeviceTag, int>
    {
    }
}
