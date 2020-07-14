using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IoT.Application.RegionAppService.DTO;
using L._52ABP.Application.Dtos;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using IoT.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using IoT.Core.Regions.Entity;

namespace IoT.Application.RegionAppService
{
    public class RegionAppService : ApplicationService, IRegionAppService
    {
        private readonly IRepository<Region, int> _regionRepository;
        public RegionAppService(IRepository<Region, int> regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public RegionDto Get(EntityDto<int> input)
        {
            var query = _regionRepository.GetAll().Where(r => r.Id == input.Id);
            var entity = query.FirstOrDefault(); ;
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("region不存在或已被删除");
            }
            return ObjectMapper.Map<RegionDto>(entity);
        }

        public PagedResultDto<RegionDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _regionRepository.GetAll().Where(r => r.IsDeleted == false);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<RegionDto>(total, ObjectMapper.Map<List<RegionDto>>(result));
        }

        public RegionDto Create(CreateRegionDto input)
        {
            var regionQuery = _regionRepository.GetAll().Where(r => r.RegionName == input.RegionName);

            if ((regionQuery.Any()) && (regionQuery.FirstOrDefault().IsDeleted == true))
            {
                var entity = regionQuery.FirstOrDefault();
                entity.IsDeleted = false;
                var result_old = _regionRepository.Update(entity);
                CurrentUnitOfWork.SaveChanges();
                return ObjectMapper.Map<RegionDto>(result_old);
            }
            if (regionQuery.Any())
            {
                throw new ApplicationException("threshold 已存在");
            }
            var region = ObjectMapper.Map<Region>(input);
            var result = _regionRepository.Insert(region);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<RegionDto>(result);
        }

        public RegionDto Update(CreateRegionDto input)
        {
            var region = _regionRepository.Get(input.Id);
            ObjectMapper.Map(input, region);
            var result = _regionRepository.Update(region);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<RegionDto>(result);
        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _regionRepository.Get(input.Id);
            _regionRepository.Delete(entity);
        }

    }
}
