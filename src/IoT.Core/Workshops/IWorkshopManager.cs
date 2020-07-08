using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Workshops
{
    public interface IWorkshopManager : IDomainService
    {
        void Delete(Workshop entity);
        public IQueryable<Workshop> GetAll();
    }
}
