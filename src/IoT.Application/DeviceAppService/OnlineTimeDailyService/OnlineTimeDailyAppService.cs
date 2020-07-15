using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using IoT.Core;
using L._52ABP.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using IoT.Application.DeviceAppService.DeviceTypeService.DTO;
using IoT.Application.DeviceAppService.OnlineTimeDailyService.Dto;

namespace IoT.Application.DeviceAppService
{
    public class OnlineTimeDailyAppService : ApplicationService, IOnlineTimeDailyAppService
    {
        private readonly IRepository<OnlineTimeDaily, int> _onlineTimeDailyRepository;
        private readonly IRepository<Device, int> _deviceRepository;
        public OnlineTimeDailyAppService(IRepository<OnlineTimeDaily, int> onlineTimeDailyRepository, IRepository<Device, int> deviceRepository)
        {
            _onlineTimeDailyRepository = onlineTimeDailyRepository;
            _deviceRepository = deviceRepository;
        }

        public OnlineTimeDailyDto Get(EntityDto<int> input)
        {
            var query = _onlineTimeDailyRepository.GetAllIncluding(o=>o.Device).Where(o=>o.Id==input.Id).Where(otd => otd.IsDeleted == false);
            var entity = query.FirstOrDefault();
            return ObjectMapper.Map<OnlineTimeDailyDto>(entity);
        }

        public PagedResultDto<OnlineTimeDailyDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _onlineTimeDailyRepository.GetAll().Where(otd=>otd.IsDeleted==false).Include(otd=>otd.Device);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<OnlineTimeDailyDto>(total, ObjectMapper.Map<List<OnlineTimeDailyDto>>(result));
            throw new NotImplementedException();
        }

        public OnlineTimeDailyDto Create(CreateOnlineTimeDailyDto input)
        {
            var onlineQuery = _onlineTimeDailyRepository.GetAll().Where(otd=>otd.Device.DeviceName == input.DeviceName).Where(otd=>otd.Date == input.Date);
            if (onlineQuery.Any())
            {
                throw new ApplicationException("该日期重复");
            }

            var deviceQuery = _deviceRepository.GetAll().Where(d=>d.DeviceName == input.DeviceName);
            if(!deviceQuery.Any())
            {
                throw new ApplicationException("该设备不存在");
            }
            
            var entity = ObjectMapper.Map<OnlineTimeDaily>(input);
            entity.Device = deviceQuery.FirstOrDefault();
            var result = _onlineTimeDailyRepository.Insert(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<OnlineTimeDailyDto>(result);
        }


        public OnlineTimeDailyDto Update(CreateOnlineTimeDailyDto input)
        {
            var deviceQuery = _deviceRepository.GetAll().Where(d => d.DeviceName == input.DeviceName);
            if (!deviceQuery.Any())
            {
                throw new ApplicationException("该设备不存在");
            }
            var device = deviceQuery.FirstOrDefault();
            var entity = ObjectMapper.Map<OnlineTimeDaily>(input);
            ObjectMapper.Map(input,entity);
            entity.Device = device;
            var result = _onlineTimeDailyRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<OnlineTimeDailyDto>(result);
            
        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _onlineTimeDailyRepository.Get(input.Id);
            _onlineTimeDailyRepository.Delete(entity);
        }
    }
}
