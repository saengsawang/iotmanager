using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Fields
{
    public interface IFieldManager : IDomainService
    {
        void Delete(Field entity);
        public IQueryable<Field> GetAll();
    }
}
