using System;
using System.Collections.Generic;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using IoT.Application.DeviceAppService.DeviceService.Dto;
using IoT.Core;
using L._52ABP.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Entities;

namespace IoT.Application.DeviceAppService.DeviceService
{
    public class DeviceAppService : ApplicationService, IDeviceAppService
    {
        private readonly IRepository<Device, int> _deviceRepository;
        private readonly IRepository<DeviceType, int> _deviceTypeRepository;
        private readonly IRepository<Gateway, int> _gatewayRepository;
        private readonly IRepository<Workshop, int> _workshopRepository;
        private readonly IRepository<Factory, int> _factoryRepository;
        private readonly IRepository<City, int> _cityRepository;

        public DeviceAppService(IRepository<Device, int> deviceRepository,
        IRepository<DeviceType, int> deviceTypeRepository,
        IRepository<Gateway, int> gatewayRepository,
        IRepository<Workshop, int> workshopRepository,
        IRepository<Factory, int> factoryRepository,
        IRepository<City, int> cityRepository)
        {
            _deviceRepository = deviceRepository;
            _deviceTypeRepository = deviceTypeRepository;
            _gatewayRepository = gatewayRepository;
            _workshopRepository = workshopRepository;
            _factoryRepository = factoryRepository;
            _cityRepository = cityRepository;
        }

        public DeviceDto Get(EntityDto<int> input)
        {
            var query = _deviceRepository.GetAll().Where(d => d.Id == input.Id)
               .Include(d => d.Gateway)
               .Include(d => d.Gateway.Workshop)
               .Include(d => d.Gateway.Workshop.Factory)
               .Include(d => d.Gateway.Workshop.Factory.City)
               .Include(d => d.Gateway.GatewayType)
               .Include(d => d.DeviceType);
            var entity = query.FirstOrDefault();
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该设备不存在或已被删除");
            }
            return ObjectMapper.Map<DeviceDto>(entity);
        }

        public PagedResultDto<DeviceDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _deviceRepository.GetAll().Where(d=>d.IsDeleted==false)
               .Include(d => d.Gateway)
               .Include(d => d.Gateway.Workshop)
               .Include(d => d.Gateway.Workshop.Factory)
               .Include(d => d.Gateway.Workshop.Factory.City)
               .Include(d => d.DeviceType);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<DeviceDto>(total, ObjectMapper.Map<List<DeviceDto>>(result));
        }

        public DeviceDto Create(CreateDeviceDto input)
        {
            var query = _deviceRepository.GetAllIncluding()
               .Where(d => d.HardwareId == input.HardwareId||d.DeviceName == input.DeviceName);
            if (query.Any())
            {
                throw new ApplicationException("设备已存在");
            }

            var workshopQuery = _workshopRepository.GetAllIncluding().Where(w => w.WorkshopName == input.WorkshopName)
                .Where(w => w.Factory.FactoryName == input.FactoryName)
                .Where(w => w.Factory.City.CityName == input.CityName);
            var workshop = workshopQuery.FirstOrDefault();
            if (workshop == null)
            {
                throw new ApplicationException("Workshop不存在");
            }

            var gatewayQuery = _gatewayRepository.GetAllIncluding().Where(g => g.Workshop.WorkshopName == input.WorkshopName)
                .Where(g => g.Workshop.Factory.FactoryName == input.FactoryName)
                .Where(g => g.Workshop.Factory.City.CityName == input.CityName)
                .Where(g => g.HardwareId == input.HardwareId || g.GatewayName == input.GatewayName);
            var gateway = gatewayQuery.FirstOrDefault();
            if (gateway==null)
            {
                throw new ApplicationException("网关不存在");
            }

            var deviceTypeQuery = _deviceTypeRepository.GetAll().Where(dt => dt.TypeName == input.DeviceTypeName);
            var deviceType = deviceTypeQuery.FirstOrDefault();
            if (deviceType == null)
            {
                throw new ApplicationException("设备类型不存在");
            }
            var device = ObjectMapper.Map<Device>(input);
            device.Gateway = gateway;
            device.DeviceType = deviceType;
            var result = _deviceRepository.Insert(device);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<DeviceDto>(result);
        }

        public DeviceDto Update(CreateDeviceDto input)
        {
            var entity = _deviceRepository.Get(input.Id);
            var cityQuery = _cityRepository.GetAll().Where(c => c.CityName == input.CityName);
            if (!cityQuery.Any())
            {
                throw new ApplicationException("City不存在");
            }

            var factoryQuery = _factoryRepository.GetAll().Where(f => f.FactoryName == input.FactoryName);
            if (!factoryQuery.Any())
            {
                throw new ApplicationException("Factory不存在");
            }

            var factory = factoryQuery.FirstOrDefault();
            if (factory != null)
            {
                factory.City = cityQuery.FirstOrDefault();
                var workshopQuery = _workshopRepository.GetAll().Where(w => w.WorkshopName == input.WorkshopName);
                if (!workshopQuery.Any())
                {
                    throw new ApplicationException("Workshop不存在");
                }

                var workshop = workshopQuery.FirstOrDefault();
                if (workshop != null)
                {
                    workshop.Factory = factory;
                    var gatewayQuery = _gatewayRepository.GetAll().Where(g => g.GatewayName == input.GatewayName);

                    if (!gatewayQuery.Any())
                    {
                        throw new ApplicationException("Gateway不存在");
                    }

                    var gateway = gatewayQuery.FirstOrDefault();
                    if (gateway != null)
                    {
                        gateway.Workshop = workshop;
                        ObjectMapper.Map(input, entity);
                        entity.Gateway = gateway;
                        var deviceTypeQuery = _deviceTypeRepository.GetAll().Where(dt => dt.TypeName == input.DeviceTypeName);
                        if (!deviceTypeQuery.Any())
                        {
                            throw new ApplicationException("该设备类型不存在");
                        }

                        var deviceType = deviceTypeQuery.FirstOrDefault();
                        if (deviceType != null)
                        {
                            entity.DeviceType = deviceType;
                        }
                    }
                }


            }
            var result = _deviceRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<DeviceDto>(result);
        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _deviceRepository.Get(input.Id);
            if (entity.IsNullOrDeleted())
            {
                throw new ArgumentException("设备不存在或已删除");
            }
            _deviceRepository.Delete(entity);
        }
    }
}
