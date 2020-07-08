using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Devices
{
    public class DeviceManager : DomainService, IDeviceManager
    {
        private readonly IDeviceRepository _deviceRepositories;
        public DeviceManager(IDeviceRepository deviceRepositories)
        {
            _deviceRepositories = deviceRepositories;
        }

        public void Delete(Device entity)
        {
            _deviceRepositories.AffiliateDelete(entity);
        }

        public Device Update(Device entity)
        {
            return _deviceRepositories.Update(entity);
        }

        public IQueryable<Device> GetAll()
        {
            return _deviceRepositories.GetAll();
        }
    }
}
