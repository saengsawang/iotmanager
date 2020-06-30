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
    class DeviceTagAppService : ApplicationService, IDeviceTagAppService
    {

        public DeviceTagAppService()
        {

        }

        public DeviceTagDto Create(CreateDeviceTagDto input)
        {
            throw new NotImplementedException();
        }

        public void Delete(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }

        public DeviceTagDto Get(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }

        public PagedResultDto<DeviceTagDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            throw new NotImplementedException();
        }

        public DeviceTagDto Update(CreateDeviceTagDto input)
        {
            throw new NotImplementedException();
        }
    }
}
