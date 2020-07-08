using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Thresholds
{
    public interface IThresholdManager : IDomainService
    {
        void Delete(Threshold entity);
        public IQueryable<Threshold> GetAll();
    }
}
