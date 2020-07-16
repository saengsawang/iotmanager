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
using IoT.Application.DeviceAppService.DeviceTagService.Dto;

namespace IoT.Application.DeviceAppService
{
    public class DeviceTagAppService : ApplicationService, IDeviceTagAppService
    {
        private readonly IRepository<DeviceTag, int> _deviceTagRepository;
        private readonly IRepository<Device, int> _deviceRepository;
        private readonly IRepository<Tag, int> _tagRepository;
        public DeviceTagAppService(IRepository<DeviceTag, int> deviceTagRepository, IRepository<Device, int> deviceRepository, IRepository<Tag, int> tagRepository)
        {
            _deviceTagRepository = deviceTagRepository;
            _deviceRepository = deviceRepository;
            _tagRepository = tagRepository;
        }

        public DeviceTagDto Get(EntityDto<int> input)
        {
            var query = _deviceTagRepository.GetAllIncluding(d => d.Device).Where(d => d.Id == input.Id).Where(dtg => dtg.IsDeleted == false);
            var entity = query.FirstOrDefault();
            
            return ObjectMapper.Map<DeviceTagDto>(entity);
        }

        

        public PagedResultDto<DeviceTagDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _deviceTagRepository.GetAll().Where(dtg=>dtg.IsDeleted==false).Include(dtg=>dtg.Device);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<DeviceTagDto>(total, ObjectMapper.Map<List<DeviceTagDto>>(result));
        }

        public DeviceTagDto Create(CreateDeviceTagDto input)
        {
            var deviceQuery = _deviceRepository.GetAll().Where(d=>d.DeviceName == input.DeviceName);
            if (!deviceQuery.Any())
            {
                throw new ApplicationException("该设备不存在");
            }

            var deviceTagQuery = _deviceTagRepository.GetAll().Where(dtg=>dtg.TagId==input.TagId);
            if (deviceTagQuery.Any())
            {
                throw new ApplicationException("该设备tag已存在");
            }
            var tagQuery = _tagRepository.GetAll().Where(tg=>tg.Id==input.TagId);
            if(!tagQuery.Any())
            {
                throw new ApplicationException("tagId不存在");
            }
            var device = deviceQuery.FirstOrDefault();
            var deviceTag = new DeviceTag()
            {
                TagId = input.TagId,
                Device = device,
                Id = input.Id,
            };
            var result = _deviceTagRepository.Insert(deviceTag);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<DeviceTagDto>(result);
        }

        public DeviceTagDto Update(CreateDeviceTagDto input)
        {
            var deviceQuery = _deviceRepository.GetAll().Where(d => d.DeviceName == input.DeviceName);
            if (!deviceQuery.Any())
            {
                throw new ApplicationException("该设备不存在");
            }

            var device = deviceQuery.FirstOrDefault();
            var deviceTag = new DeviceTag()
            {
                TagId = input.TagId,
                Device = device,
                Id = input.Id
            };
            var result = _deviceTagRepository.Update(deviceTag);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<DeviceTagDto>(result);

        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _deviceTagRepository.Get(input.Id);
            _deviceTagRepository.Delete(entity);
        }

    }
}
