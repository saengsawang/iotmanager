using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.ThresholdAppService.SevetityAppService.DTO
{
    public class CreateSeverityDto : EntityDto<int>
    {
        public string SeverityName { get; set; }
    }
}
