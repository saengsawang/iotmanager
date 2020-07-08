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
        public DeviceRepository(IFieldManager fieldManager, IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _fieldManager = fieldManager;
        }

        public void AffiliateDelete(Device entity)
        {
            var query = _fieldManager.GetAll().Where(f => f.DeviceId == entity.Id);
            ArrayList list = new ArrayList(query.Count());
            if (query.Any())
            {
                foreach (var field in query)
                {
                    list.Add((Field)field);
                }
            }
            foreach (var field in list)
            {
                _fieldManager.Delete((Field)field);
            }
            Delete(entity);
        }
    }
}
