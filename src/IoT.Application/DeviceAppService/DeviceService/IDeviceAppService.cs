using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IoT.Application.DeviceAppService.DeviceService.Dto;
using L._52ABP.Application.Dtos;

namespace IoT.Application.DeviceAppService.DeviceService
{
    public interface IDeviceAppService:ICrudAppService<DeviceDto,int, PagedSortedAndFilteredInputDto, CreateDeviceDto>
    {
        void BatchDelete(int[] inputs);
    }
}
