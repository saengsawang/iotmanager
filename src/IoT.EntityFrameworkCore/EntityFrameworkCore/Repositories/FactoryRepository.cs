using System;
using System.Collections;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Factories;
using IoT.Core.Workshops;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class FactoryRepository : IoTRepositoryBase<Factory, int>, IFactoryRepository
    {
        private readonly IWorkshopManager _workshopManager;

        public FactoryRepository(IWorkshopManager workshopManager,IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _workshopManager = workshopManager;
        }

        public void AffiliateDelete(Factory entity)
        {
            var query = _workshopManager.GetAll().Where(w => w.FactoryId == entity.Id);
            ArrayList list = new ArrayList(query.Count());
            if (query.Any())
            {
                foreach (var workshop in query)
                {
                    list.Add((Workshop)workshop);
                }
            }
            foreach (var workshop in list)
            {
                _workshopManager.Delete((Workshop)workshop);
            }
            Delete(entity);
        }
    }
}
