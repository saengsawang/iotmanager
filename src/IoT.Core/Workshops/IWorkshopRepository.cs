using System;
using Abp.Domain.Repositories;

namespace IoT.Core.Workshops
{
    public interface IWorkshopRepository : IRepository<Workshop, int>
    {
        void AffiliateDelete(Workshop entity);
    }
}
