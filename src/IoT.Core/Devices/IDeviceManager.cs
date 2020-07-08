using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Devices
{
    public interface IDeviceManager : IDomainService
    {
        void Delete(Device entity);
        public IQueryable<Device> GetAll();
    }
}
