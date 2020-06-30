using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using IoT.Application.DeviceAppService.DeviceService.Dto;
using L._52ABP.Application.Dtos;

namespace IoT.Application.DeviceAppService.DeviceService
{
    interface IDeviceAppService:ICrudAppService<DeviceDto,int, PagedSortedAndFilteredInputDto, CreateDeviceDto>
    {
    }
}
