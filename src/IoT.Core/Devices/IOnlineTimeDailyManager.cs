using System;
using System.Linq;
using Abp.Domain.Services;

namespace IoT.Core.Devices
{
    public interface IOnlineTimeDailyManager : IDomainService
    {
        void Delete(OnlineTimeDaily entity);
        public IQueryable<OnlineTimeDaily> GetAll();
    }
}
