using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.DeviceAppService.DeviceTypeService.DTO
{
    public class CreateDeviceTypeDto : EntityDto<int>
    {
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public decimal OfflineTime { get; set; }
    }
}
