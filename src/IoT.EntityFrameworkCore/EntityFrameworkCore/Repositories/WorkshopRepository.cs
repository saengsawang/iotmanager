using System;
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
            if (query.Any())
            {
                foreach (var gateway in query)
                {
                    _gatewayRepository.AffiliateDelete(gateway);
                }
            }
            Delete(entity);
        }
    }
}
