using System;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace IoT.Core.Devices
{
    public class OnlineTimeDailyManager : DomainService, IOnlineTimeDailyManager
    {
        private readonly IRepository<OnlineTimeDaily, int> _onlineTimeDailyRepository;
        public OnlineTimeDailyManager(IRepository<OnlineTimeDaily, int> onlineTimeDailyRepository)
        {
            _onlineTimeDailyRepository = onlineTimeDailyRepository;
        }

        public void Delete(OnlineTimeDaily entity)
        {
            _onlineTimeDailyRepository.Delete(entity);
        }

        public OnlineTimeDaily Update(OnlineTimeDaily entity)
        {
            return _onlineTimeDailyRepository.Update(entity);
        }

        public IQueryable<OnlineTimeDaily> GetAll()
        {
            return _onlineTimeDailyRepository.GetAll();
        }
    }
}
