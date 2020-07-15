using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Devices
{
    public class DeviceTagManager : DomainService, IDeviceTagManager
    {
        private readonly IDeviceTagRepository _deviceTagRepository;
        public DeviceTagManager(IDeviceTagRepository deviceTagRepository)
        {
            _deviceTagRepository = deviceTagRepository;
        }

        public void Delete(DeviceTag entity)
        {
            _deviceTagRepository.Delete(entity);
        }

        public DeviceTag Update(DeviceTag entity)
        {
            return _deviceTagRepository.Update(entity);
        }

        public IQueryable<DeviceTag> GetAll()
        {
            return _deviceTagRepository.GetAll();
        }
    }
}
