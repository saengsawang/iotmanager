using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.DeviceAppService.DeviceTypeService.DTO
{
    public class DeviceTypeDto : EntityDto<int>
    {
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public decimal OfflineTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
