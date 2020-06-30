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
using IoT.Application.DeviceAppService.OnlineTimeDailyService.Dto;

namespace IoT.Application.DeviceAppService
{
    class OnlineTimeDailyAppService : ApplicationService, IOnlineTimeDailyAppService
    {
        public OnlineTimeDailyAppService()
        {

        }

        public OnlineTimeDailyDto Create(CreateOnlineTimeDailyDto input)
        {
            throw new NotImplementedException();
        }

        public void Delete(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }

        public OnlineTimeDailyDto Get(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }

        public PagedResultDto<OnlineTimeDailyDto> GetAll(PagedSortedAndFilteredInputDto input)
        {
            throw new NotImplementedException();
        }

        public OnlineTimeDailyDto Update(CreateOnlineTimeDailyDto input)
        {
            throw new NotImplementedException();
        }
    }
}
