using System;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Factories.Entity;
using IoT.Core.Workshops.Entity;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class FactoryRepository : IoTRepositoryBase<Factory, int>, IFactoryRepository
    {
        private readonly IWorkshopRepository _workshopRepository;

        public FactoryRepository(IWorkshopRepository workshopRepository,IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _workshopRepository = workshopRepository;
        }

        public void AffiliateDelete(Factory entity)
        {
            var query = _workshopRepository.GetAll().Where(w => w.FactoryId == entity.Id);
            if (query.Any())
            {
                foreach (var workshop in query)
                {
                    _workshopRepository.AffiliateDelete(workshop);
                }
            }
            Delete(entity);
        }
    }
}
