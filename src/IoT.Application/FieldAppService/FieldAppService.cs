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
using AutoMapper;
using Abp.Domain.Entities;
using IoT.Core.Fields;
using IoT.Core.Devices;
using Microsoft.AspNetCore.Mvc;

namespace IoT.Application.FieldAppService
{
    public class FieldAppService : ApplicationService, IFieldAppService
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IFieldManager _fieldManager;
        public FieldAppService(IFieldRepository fieldRepository,IDeviceRepository deviceRepository, IFieldManager fieldManager)
        {
            _fieldRepository = fieldRepository;
            _deviceRepository = deviceRepository;
            _fieldManager = fieldManager;
        }

        public FieldDto Get(EntityDto<int> input)
        {
           var query = _fieldRepository.GetAllIncluding(f => f.Device).Where(f => f.Id == input.Id);
            var entity = query.FirstOrDefault();
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该field不存在或已被删除");
            }
            return ObjectMapper.Map<FieldDto>(entity);
        }

        public FieldDto GetByName(string fieldName)
        {
            var query = _fieldRepository.GetAllIncluding(f => f.Device).Where(f => f.FieldName.Contains(fieldName));
            var entity = query.FirstOrDefault();
            if (entity.IsNullOrDeleted())
            {
                throw new ApplicationException("该field不存在或已被删除");
            }
            return ObjectMapper.Map<FieldDto>(entity);
        }

        public PagedResultDto<FieldDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            var query = _fieldRepository.GetAll().Where(f=>f.IsDeleted==false).Include(f=>f.Device);
            var total = query.Count();
            var result = input.Sorting != null
                ? query.OrderBy(input.Sorting).AsNoTracking().PageBy(input).ToList()
                : query.PageBy(input).ToList();
            return new PagedResultDto<FieldDto>(total, ObjectMapper.Map<List<FieldDto>>(result));
        }


        [HttpGet]
        public PagedResultDto<FieldDto> GetByDevice(string DeviceName)
        {
            var deviceQuery = _deviceRepository.GetAll().Where(d => d.DeviceName == DeviceName).Where(g => g.IsDeleted == false);
            if (!deviceQuery.Any())
            {
                throw new ApplicationException("城市不存在或已被删除");
            }
            var query = _fieldRepository.GetAll().Where(d => d.IsDeleted == false).Where(f => f.Device.DeviceName == DeviceName)
               .Include(f => f.Device);
            var total = query.Count();
            var result = query.ToList();
            return new PagedResultDto<FieldDto>(total, ObjectMapper.Map<List<FieldDto>>(result));
        }

        [HttpGet]
        public long GetNumber()
        {
            var query = _fieldRepository.GetAll().Where(f => f.IsDeleted == false);
            return query.Count();
        }

        public FieldDto Create(FieldDto input)
        {
            var fieldQuery = _fieldRepository.GetAll().Where(f=>f.FieldName == input.FieldName);
            if ((fieldQuery.Any()) && (fieldQuery.FirstOrDefault().IsDeleted == true))
            {
                var entity_old = fieldQuery.FirstOrDefault();
                entity_old.IsDeleted = false;
                var result_old = _fieldRepository.Update(entity_old);
                CurrentUnitOfWork.SaveChanges();
                return ObjectMapper.Map<FieldDto>(result_old);
            }
            if (fieldQuery.Any())
            {
                throw new ApplicationException("field 已存在");
            }
            var deviceQuery = _deviceRepository.GetAll().Where(d => d.DeviceName == input.DeviceName);
            if(!deviceQuery.Any())
            {
                throw new ApplicationException("设备不存在");
            }
            
            var device = deviceQuery.FirstOrDefault();
            var field = new Field()
            {
                FieldName = input.FieldName,
                IndexId = input.IndexId,
                Device = device
            };
            
            var result = _fieldRepository.Insert(field);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<FieldDto>(result);
        }


        public FieldDto Update(FieldDto input)
        {
            var deviceQuery = _deviceRepository.GetAll().Where(d => d.DeviceName == input.DeviceName);
            if (!deviceQuery.Any())
            {
                throw new ApplicationException("设备不存在");
            }
            var device = deviceQuery.FirstOrDefault();
            var field = new Field() {
                FieldName = input.FieldName,
                IndexId = input.IndexId,
                Device = device,
                Id = input.Id
            } ;
            
            
            var result = _fieldRepository.Update(field);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<FieldDto>(result);
        }

        public void Delete(EntityDto<int> input)
        {
            var entity = _fieldRepository.Get(input.Id);
            _fieldManager.Delete(entity);
        }

        [HttpDelete]
        public void BatchDelete(int[] inputs)
        {
            foreach (var input in inputs)
            {
                var entity = _fieldRepository.Get(input);
                _fieldManager.Delete(entity);
            }
        }
    }
}
