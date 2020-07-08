using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Factories
{
    public class FactoryManager : DomainService, IFactoryManager
    {
        private readonly IFactoryRepository _factoryRepositories;
        public FactoryManager(IFactoryRepository factoryRepositories)
        {
            _factoryRepositories = factoryRepositories;
        }

        public void Delete(Factory entity)
        {
            _factoryRepositories.AffiliateDelete(entity);
        }

        public Factory Update(Factory entity)
        {
            return _factoryRepositories.Update(entity);
        }

        public IQueryable<Factory> GetAll()
        {
            return _factoryRepositories.GetAll();
        }
    }
}
