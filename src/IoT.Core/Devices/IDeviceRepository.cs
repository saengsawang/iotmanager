using System;
using Abp.Domain.Repositories;

namespace IoT.Core.Devices
{
    public interface IDeviceRepository : IRepository<Device, int>
    {
        void AffiliateDelete(Device entity);
    }
}
