using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using IoT.Application.DeviceAppService.DeviceTagService.Dto;
using L._52ABP.Application.Dtos;

namespace IoT.Application.DeviceAppService
{
    public interface IDeviceTagAppService : ICrudAppService<DeviceTagDto, int, PagedSortedAndFilteredInputDto, CreateDeviceTagDto>
    {
    }
}
