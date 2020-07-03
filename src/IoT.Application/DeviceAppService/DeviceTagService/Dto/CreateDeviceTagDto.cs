using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.DeviceAppService.DeviceTagService.Dto
{
    public class CreateDeviceTagDto : EntityDto<int>
    {
        public int TagId { get; set; }
        public string DeviceName { get; set; }
    }
}
