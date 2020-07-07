using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using IoT.Application.FactoryAppService.DTO;
using IoT.Core;
using IoT.Core.Cities;
using IoT.Core.Factories.Entity;
using L._52ABP.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IoT.Application.FactoryAppService
{
    public class FactoryAppService:ApplicationService,IFactoryAppService
    {
        private readonly IFactoryRepository _factoryRepository;
        private readonly ICityRepository _cityRepository;
        public FactoryAppService(IFactoryRepository factoryRepository, ICityRepository cityRepository)
        {
            _factoryRepository = factoryRepository;
            _cityRepository = cityRepository;
        }

        public FactoryDto Get(EntityDto<int> input)
        {
            var query = _factoryRepository.GetAllIncluding(e => e.City).Where(e => e.Id == input.Id);
            var entity = query.FirstOrDefault();
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该设备不存在或已被删除");
            }
            var result = ObjectMapper.Map<FactoryDto>(entity);
            return result;
        }

        public PagedResultDto<FactoryDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _factoryRepository.GetAllIncluding(q => q.City).Where(f=>f.IsDeleted==false);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking<Factory>().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<FactoryDto>(total, ObjectMapper.Map<List<FactoryDto>>(result));
        }


        public FactoryDto Create(CreateFactoryDto input)
        {
            var query = _cityRepository.GetAll().Where(c => c.CityName == input.CityName);
            var city = query.FirstOrDefault();
            if (city.IsNullOrDeleted())
            {
                throw new ApplicationException("城市不存在");
            }
            var entity = ObjectMapper.Map<Factory>(input);
            entity.City = city;
            var result = _factoryRepository.Insert(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<FactoryDto>(result);
        }

        public FactoryDto Update(CreateFactoryDto input)
        {
            var query = _cityRepository.GetAll().Where(c => c.CityName == input.CityName);
            var city = query.FirstOrDefault();
            if (city.IsNullOrDeleted())
            {
                throw new ApplicationException("城市不存在");
            }
            var entity = _factoryRepository.Get(input.Id);
            ObjectMapper.Map(input, entity);
            entity.City = city;
            var result = _factoryRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<FactoryDto>(result);
        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _factoryRepository.Get(input.Id);
            _factoryRepository.AffiliateDelete(entity);
        }
    }
}
