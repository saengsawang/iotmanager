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

namespace IoT.Application.DeviceAppService.DeviceTypeService
{
    public class DeviceTypeAppService : ApplicationService, IDeviceTypeAppService
    {
        private readonly IRepository<DeviceType, int> _deviceTypeRepository;
        public DeviceTypeAppService(IRepository<DeviceType, int> deviceTypeRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
        }

        public DeviceTypeDto Get(EntityDto<int> input)
        {
            var entity = _deviceTypeRepository.Get(input.Id);
            return ObjectMapper.Map<DeviceTypeDto>(entity);
        }

        public DeviceTypeDto GetByName(string typeName)
        {
            var query = _deviceTypeRepository.GetAll().Where(dt=>dt.TypeName.Contains(typeName));
            var entity = query.FirstOrDefault();
            
            return ObjectMapper.Map<DeviceTypeDto>(entity);
        }

        public PagedResultDto<DeviceTypeDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _deviceTypeRepository.GetAll();
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<DeviceTypeDto>(total, ObjectMapper.Map<List<DeviceTypeDto>>(result));
        }

        public DeviceTypeDto Create(CreateDeviceTypeDto input)
        {
            var query = _deviceTypeRepository.GetAll().Where(dt => dt.TypeId == input.TypeId);
            if (query.Any())
            {
                throw new ApplicationException("该设备类型已存在");
            }

            var entity = ObjectMapper.Map<DeviceType>(input);
            var result = _deviceTypeRepository.Insert(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<DeviceTypeDto>(result);
        }

        public DeviceTypeDto Update(CreateDeviceTypeDto input)
        {
            var entity = ObjectMapper.Map<DeviceType>(input);
            ObjectMapper.Map(input, entity);
            var result = _deviceTypeRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<DeviceTypeDto>(result);
        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _deviceTypeRepository.Get(input.Id);
            _deviceTypeRepository.Delete(entity);
        }

    }
}
