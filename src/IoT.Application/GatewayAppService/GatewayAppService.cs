using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using IoT.Application.GatewayAppService.DTO;
using IoT.Core;
using L._52ABP.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Entities;
using IoT.Core.Gateways.Entity;

namespace IoT.Application.GatewayAppService
{
    public class GatewayAppService:ApplicationService,IGatewayAppService
    {
        private readonly IRepository<GatewayType, int> _gatewayTypeRepository;
        private readonly IGatewayRepository _gatewayRepository;
        private readonly IRepository<Workshop, int> _workshopRepository;
        private readonly IRepository<Factory, int> _factoryRepository;
        private readonly IRepository<City, int> _cityRepository;

        public GatewayAppService(IRepository<GatewayType, int> gatewayTypeRepository, IGatewayRepository gatewayRepository, IRepository<Workshop, int> workshopRepository, IRepository<Factory, int> factoryRepository, IRepository<City, int> cityRepository)
        {
            _gatewayTypeRepository = gatewayTypeRepository;
            _gatewayRepository = gatewayRepository;
            _workshopRepository = workshopRepository;
            _factoryRepository = factoryRepository;
            _cityRepository = cityRepository;
        }


        public GatewayDto Get(EntityDto<int> input)
        {
            var query = _gatewayRepository.GetAll().Where(g => g.Id == input.Id)
                .Include(g => g.Workshop)
                .Include(g => g.Workshop.Factory)
                .Include(g => g.Workshop.Factory.City)
                .Include(g=>g.GatewayType);
            var entity = query.FirstOrDefault();
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该设备不存在或已被删除");
            }
            return ObjectMapper.Map<GatewayDto>(entity);
        }

        public PagedResultDto<GatewayDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query=_gatewayRepository.GetAll().Where(g=>g.IsDeleted==false)
                .Include(g => g.Workshop)
                .Include(g => g.Workshop.Factory)
                .Include(g => g.Workshop.Factory.City)
                .Include(g=>g.GatewayType);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<GatewayDto>(total, ObjectMapper.Map<List<GatewayDto>>(result));
        }

        public GatewayDto Create(CreateGatewayDto input)
        {
            var query = _gatewayRepository.GetAllIncluding().Where(g => g.HardwareId == input.HardwareId || g.GatewayName == input.GatewayName);
            var gateway_old = query.FirstOrDefault();
            
            if (query.Any()&&gateway_old.IsDeleted==false)
            {
                throw new ApplicationException("网关已存在");
            }else if (gateway_old.IsDeleted==true)
            {
                gateway_old.IsDeleted = false;
                var result_1 = _gatewayRepository.Update(gateway_old);
                return ObjectMapper.Map<GatewayDto>(result_1);
            }
            

            var workshopQuery = _workshopRepository.GetAllIncluding().Where(w => w.WorkshopName == input.WorkshopName)
                .Where(w => w.Factory.FactoryName == input.FactoryName)
                .Where(w => w.Factory.City.CityName == input.CityName);
            var workshop = workshopQuery.FirstOrDefault();
            if (workshop==null)
            {
                throw new ApplicationException("Workshop不存在");
            }

            var gatewayTypeQuery = _gatewayTypeRepository.GetAll().Where(gt => gt.TypeName == input.GatewayTypeName);
            var gatewayType = gatewayTypeQuery.FirstOrDefault();
            if (gatewayType==null)
            {
                throw new ApplicationException("网关类型不存在");
            }
            var gateway = ObjectMapper.Map<Gateway>(input);
            gateway.Workshop = workshop;
            gateway.GatewayType = gatewayType;
            var result = _gatewayRepository.Insert(gateway);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<GatewayDto>(result);
        }

        public GatewayDto Update(UpdateGatewayDto input)
        {
            var entity = _gatewayRepository.Get(input.Id);
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
                    ObjectMapper.Map(input, entity);
                    entity.Workshop = workshop;
                }
            }

            var result = _gatewayRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<GatewayDto>(result);

        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _gatewayRepository.Get(input.Id);
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该设备不存在或已被删除");
            }
            _gatewayRepository.AffiliateDelete(entity);
        }
    }
}
