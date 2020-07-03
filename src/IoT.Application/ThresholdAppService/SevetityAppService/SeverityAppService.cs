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
using IoT.Application.DeviceAppService;
using IoT.Application.ThresholdAppService.SevetityAppService.DTO;

namespace IoT.Application.ThresholdAppService.SevetityAppService
{
    public class SeverityAppService : ApplicationService, ISeverityAppService
    {
        private readonly IRepository<Severity, int> _severityRepository;
        public SeverityAppService(IRepository<Severity, int> severityRepository)
        {
            _severityRepository = severityRepository;
        }

        public SeverityDto Get(EntityDto<int> input)
        {
            var entity = _severityRepository.Get(input.Id);
            return ObjectMapper.Map<SeverityDto>(entity);
        }

        public PagedResultDto<SeverityDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _severityRepository.GetAll();
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<SeverityDto>(total, ObjectMapper.Map<List<SeverityDto>>(result));
        }

        public SeverityDto Create(CreateSeverityDto input)
        {
            var severityQuery = _severityRepository.GetAll().Where(t => t.SeverityName == input.SeverityName);
            if (severityQuery.Any())
            {
                throw new ApplicationException("Severity已存在");
            }

            var severity = ObjectMapper.Map<Severity>(input);
            var result = _severityRepository.Insert(severity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<SeverityDto>(result);
        }

        public SeverityDto Update(CreateSeverityDto input)
        {
            var entity = _severityRepository.Get(input.Id);
            ObjectMapper.Map(input, entity);
            var result = _severityRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<SeverityDto>(result);
        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _severityRepository.Get(input.Id);
            _severityRepository.Delete(entity);
        }

    }
}
