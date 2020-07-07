using System;
using System.Collections;
using System.Linq;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Gateways.Entity;
using IoT.Core.Workshops.Entity;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class WorkshopRepository : IoTRepositoryBase<Workshop, int>, IWorkshopRepository
    {
        private readonly IGatewayRepository _gatewayRepository;
        public WorkshopRepository(IGatewayRepository gatewayRepository,IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _gatewayRepository = gatewayRepository;
        }

        public void AffiliateDelete(Workshop entity)
        {
            var query = _gatewayRepository.GetAll().Where(g=>g.WorkshopId == entity.Id);
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
                _gatewayRepository.AffiliateDelete((Gateway)gateway);
            }
            Delete(entity);
        }
    }
}
