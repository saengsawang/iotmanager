using System;
using Abp.Application.Services;
using IoT.Application.DeviceAppService.DeviceTypeService.DTO;
using L._52ABP.Application.Dtos;

namespace IoT.Application.DeviceAppService.DeviceTypeService
{
    public interface IDeviceTypeAppService : ICrudAppService<DeviceTypeDto, int, PagedSortedAndFilteredInputDto, CreateDeviceTypeDto>
    {
        DeviceTypeDto GetByName(string typeName);
    }
}
