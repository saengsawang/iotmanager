using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.ThresholdAppService.SevetityAppService.DTO
{
    public class SeverityDto : EntityDto<int>
    {
        public string SeverityName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
