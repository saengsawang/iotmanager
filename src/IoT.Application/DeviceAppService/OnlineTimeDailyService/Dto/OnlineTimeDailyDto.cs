using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.DeviceAppService.OnlineTimeDailyService.Dto
{
    public class OnlineTimeDailyDto : EntityDto<int>
    {
        public string DeviceName { get; set; }
        public decimal OnlineTime { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
