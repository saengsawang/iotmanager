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
        long GetNumber();
        PagedResultDto<DeviceDto> GetByType(string deviceType);
        PagedResultDto<DeviceDto> GetByCity(string CityName);
        PagedResultDto<DeviceDto> GetByFactory(string FactoryName);
        PagedResultDto<DeviceDto> GetByWorkshop(string WorkshopName);
        PagedResultDto<DeviceDto> GetByGateway(string GatewayName);
        Object GetDeviceLocationByDeviceId(EntityDto<int> input);
        List<object> GetFieldOptions();
        DeviceDto UpdateLastConnectionTimeByDeviceId(int deviceId);
        string GetPicture(int deviceId);
        String UploadPicture(string base64Image, int deviceId);
    }
}
