using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Devices
{
    public interface IDeviceTagManager : IDomainService
    {
        void Delete(DeviceTag entity);
        public IQueryable<DeviceTag> GetAll();
    }
}
