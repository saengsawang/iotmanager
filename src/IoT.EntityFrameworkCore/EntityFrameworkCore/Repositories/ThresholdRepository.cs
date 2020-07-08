using System;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Thresholds;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class ThresholdRepository : IoTRepositoryBase<Threshold, int>, IThresholdRepository
    {
        public ThresholdRepository(IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
