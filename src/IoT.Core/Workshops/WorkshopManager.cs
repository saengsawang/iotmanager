using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Workshops
{
    public class WorkshopManager : DomainService, IWorkshopManager
    {
        private readonly IWorkshopRepository _workshopRepositories;
        public WorkshopManager(IWorkshopRepository workshopRepositories)
        {
            _workshopRepositories = workshopRepositories;
        }

        public void Delete(Workshop entity)
        {
            _workshopRepositories.AffiliateDelete(entity);
        }

        public Workshop Update(Workshop entity)
        {
            return _workshopRepositories.Update(entity);
        }

        public IQueryable<Workshop> GetAll()
        {
            return _workshopRepositories.GetAll();
        }
    }
}
