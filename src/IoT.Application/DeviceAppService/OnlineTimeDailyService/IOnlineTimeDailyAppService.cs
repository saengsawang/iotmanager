using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using IoT.Application.DeviceAppService.OnlineTimeDailyService.Dto;
using L._52ABP.Application.Dtos;

namespace IoT.Application.DeviceAppService
{
    public interface IOnlineTimeDailyAppService : ICrudAppService<OnlineTimeDailyDto, int, PagedSortedAndFilteredInputDto, CreateOnlineTimeDailyDto>
    {
    }
}
