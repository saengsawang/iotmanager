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
using IoT.Application.FieldAppService.DTO;
using IoT.Application.ThresholdAppService.DTO;
using Abp.Domain.Entities;
using IoT.Core.Fields;
using IoT.Core.Thresholds;
using Microsoft.AspNetCore.Mvc;

namespace IoT.Application.ThresholdAppService
{
    public class ThresholdAppService : ApplicationService, IThresholdAppService
    {
        private readonly IThresholdRepository _thresholdRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IRepository<Severity, int> _severityRepository;
        public ThresholdAppService(IThresholdRepository thresholdRepository, IFieldRepository fieldRepository, IRepository<Severity, int> severityRepository)
        {
            _thresholdRepository = thresholdRepository;
            _fieldRepository = fieldRepository;
            _severityRepository = severityRepository;
        }

        public ThresholdDto Get(EntityDto<int> input)
        {
            var query = _thresholdRepository.GetAllIncluding(t=>t.Field).Include(t=>t.Severity).Where(t => t.Id == input.Id);
            var entity = query.FirstOrDefault(); ;
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该threshold不存在或已被删除");
            }
            return ObjectMapper.Map<ThresholdDto>(entity);
        }

        public ThresholdDto GetByName(string ruleName)
        {
            var query = _thresholdRepository.GetAllIncluding(t => t.Field).Include(t => t.Severity).Where(t => t.RuleName.Contains(ruleName));
            var entity = query.FirstOrDefault(); ;
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该threshold不存在或已被删除");
            }
            return ObjectMapper.Map<ThresholdDto>(entity);
        }

        public PagedResultDto<ThresholdDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _thresholdRepository.GetAll().Where(t=>t.IsDeleted==false).Include(t => t.Field)
                .Include(t=>t.Severity);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<ThresholdDto>(total, ObjectMapper.Map<List<ThresholdDto>>(result));
        }

        public ThresholdDto Create(ThresholdDto input)
        {
            var thresholdQuery = _thresholdRepository.GetAll().Where(t=>t.RuleName == input.RuleName);

            if ((thresholdQuery.Any()) && (thresholdQuery.FirstOrDefault().IsDeleted == true))
            {
                var entity = thresholdQuery.FirstOrDefault();
                entity.IsDeleted = false;
                var result_old = _thresholdRepository.Update(entity);
                CurrentUnitOfWork.SaveChanges();
                return ObjectMapper.Map<ThresholdDto>(result_old);
            }
            if (thresholdQuery.Any())
            {
                throw new ApplicationException("threshold 已存在");
            }
            var fieldQuery = _fieldRepository.GetAll().Where(f=>f.FieldName==input.FieldName);
            if(!fieldQuery.Any())
            {
                throw new ApplicationException("field 不存在");
            }

            var severityQuery = _severityRepository.GetAll().Where(s => s.SeverityName == input.SeverityName);
            if(!severityQuery.Any())
            {
                throw new ApplicationException("severity 不存在");
            }
            var field = fieldQuery.FirstOrDefault();
            var severity = severityQuery.FirstOrDefault();
            var threshold = new Threshold() {
                Field = field,
                Operator = input.Operator,
                RuleName = input.RuleName,
                Severity = severity,
                Id = input.Id,
                ThresholdValue = input.ThresholdValue,
                Description = input.Description
            };
            
            var result = _thresholdRepository.Insert(threshold);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ThresholdDto>(result);
            
        }


        public ThresholdDto Update(ThresholdDto input)
        {
            var fieldQuery = _fieldRepository.GetAll().Where(f => f.FieldName == input.FieldName);
            if (!fieldQuery.Any())
            {
                throw new ApplicationException("field 不存在");
            }

            var severityQuery = _severityRepository.GetAll().Where(s => s.SeverityName == input.SeverityName);
            if (!severityQuery.Any())
            {
                throw new ApplicationException("severity 不存在");
            }
            var field = fieldQuery.FirstOrDefault();
            var severity = severityQuery.FirstOrDefault();
            var threshold = new Threshold()
            {
                Field = field,
                Operator = input.Operator,
                RuleName = input.RuleName,
                Severity = severity,
                Id = input.Id,
                ThresholdValue = input.ThresholdValue,
                Description = input.Description
            };
            var result = _thresholdRepository.Update(threshold);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<ThresholdDto>(result);
        }

        [HttpDelete]
        public void Delete(EntityDto<int> input)
        {
            var entity = _thresholdRepository.Get(input.Id);
            _thresholdRepository.Delete(entity);
        }
    }
}
