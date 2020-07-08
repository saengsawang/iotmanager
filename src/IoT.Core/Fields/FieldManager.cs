using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Fields
{
    public class FieldManager : DomainService, IFieldManager
    {
        private readonly IFieldRepository _fieldRepositories;
        public FieldManager(IFieldRepository fieldRepositories)
        {
            _fieldRepositories = fieldRepositories;
        }

        public void Delete(Field entity)
        {
            _fieldRepositories.AffiliateDelete(entity);
        }

        public Field Update(Field entity)
        {
            return _fieldRepositories.Update(entity);
        }

        public IQueryable<Field> GetAll()
        {
            return _fieldRepositories.GetAll();
        }
    }
}
