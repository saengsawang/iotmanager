using System;
using Abp.Domain.Repositories;

namespace IoT.Core.Thresholds
{
    public interface IThresholdRepository : IRepository<Threshold, int>
    {
        
    }
}
