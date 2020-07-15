using System;
using System.Collections;
using System.Linq;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Devices;
using IoT.Core.Fields;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class DeviceRepository : IoTRepositoryBase<Device, int>, IDeviceRepository
    {
        private readonly IFieldManager _fieldManager;
        private readonly IDeviceTagManager _deviceTagManager;
        private readonly IOnlineTimeDailyManager _onlineTimeDailyManager;

        public DeviceRepository(IFieldManager fieldManager, IDeviceTagManager deviceTagManager, IOnlineTimeDailyManager onlineTimeDailyManager, IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _fieldManager = fieldManager;
            _deviceTagManager = deviceTagManager;
            _onlineTimeDailyManager = onlineTimeDailyManager;
        }

        public void AffiliateDelete(Device entity)
        {
            var fieldQuery = _fieldManager.GetAll().Where(f => f.DeviceId == entity.Id);
            ArrayList listField = new ArrayList(fieldQuery.Count());
            if (fieldQuery.Any())
            {
                foreach (var field in fieldQuery)
                {
                    listField.Add((Field)field);
                }
            }

            var deviceTagQuery = _deviceTagManager.GetAll().Where(dt => dt.DeviceId == entity.Id);
            ArrayList listDT = new ArrayList(deviceTagQuery.Count());
            if (deviceTagQuery.Any())
            {
                foreach(var dt in deviceTagQuery)
                {
                    listDT.Add((DeviceTag)dt);
                }
            }

            var otdQuery = _onlineTimeDailyManager.GetAll().Where(otd => otd.DeviceId == entity.Id);
            ArrayList listOTD = new ArrayList(otdQuery.Count());
            if (otdQuery.Any())
            {
                foreach (var otd in otdQuery)
                {
                    listOTD.Add((OnlineTimeDaily)otd);
                }
            }

            foreach (var field in listField)
            {
                _fieldManager.Delete((Field)field);
            }

            foreach(var dt in listDT)
            {
                _deviceTagManager.Delete((DeviceTag)dt);
            }

            foreach (var otd in listOTD)
            {
                _onlineTimeDailyManager.Delete((OnlineTimeDaily)otd);
            }

            Delete(entity);
        }
    }
}
