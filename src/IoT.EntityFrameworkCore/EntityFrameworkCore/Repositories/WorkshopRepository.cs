using System;
using System.Collections;
using System.Linq;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Gateways;
using IoT.Core.Workshops;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class WorkshopRepository : IoTRepositoryBase<Workshop, int>, IWorkshopRepository
    {
        private readonly IGatewayManager _gatewayManager;
        public WorkshopRepository(IGatewayManager gatewayManager,IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _gatewayManager = gatewayManager;
        }

        public void AffiliateDelete(Workshop entity)
        {
            var query = _gatewayManager.GetAll().Where(g=>g.WorkshopId == entity.Id);
            ArrayList list = new ArrayList(query.Count());
            if (query.Any())
            {
                foreach (var gateway in query)
                {
                    list.Add((Gateway)gateway);
                }
            }
            foreach (var gateway in list)
            {
                _gatewayManager.Delete((Gateway)gateway);
            }
            Delete(entity);
        }
    }
}
