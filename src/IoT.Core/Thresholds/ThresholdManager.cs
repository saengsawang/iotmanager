using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Thresholds
{
    public class ThresholdManager : DomainService, IThresholdManager
    {
        private readonly IThresholdRepository _thresholdRepositories;
        public ThresholdManager(IThresholdRepository thresholdRepositories)
        {
            _thresholdRepositories = thresholdRepositories;
        }

        public void Delete(Threshold entity)
        {
            _thresholdRepositories.Delete(entity);
        }

        public Threshold Update(Threshold entity)
        {
            return _thresholdRepositories.Update(entity);
        }

        public IQueryable<Threshold> GetAll()
        {
            return _thresholdRepositories.GetAll();
        }
    }
}
