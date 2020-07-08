using System;
using System.Collections;
using System.Linq;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Fields;
using IoT.Core.Thresholds;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class FieldRepository : IoTRepositoryBase<Field, int>, IFieldRepository
    {
        private readonly IThresholdManager _thresholdManager;
        public FieldRepository(IThresholdManager thresholdManager, IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _thresholdManager = thresholdManager;
        }

        public void AffiliateDelete(Field entity)
        {
            var query = _thresholdManager.GetAll().Where(t => t.FieldId == entity.Id);
            ArrayList list = new ArrayList(query.Count());
            if (query.Any())
            {
                foreach (var threshold in query)
                {
                    list.Add((Threshold)threshold);
                }
            }
            foreach (var threshold in list)
            {
                _thresholdManager.Delete((Threshold)threshold);
            }
            Delete(entity);
        }
    }
}
