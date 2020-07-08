using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Factories
{
    public interface IFactoryManager : IDomainService
    {
        void Delete(Factory entity);
        public IQueryable<Factory> GetAll();
    }
}
