using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.ThresholdAppService.DTO
{
    public class CreateThresholdDto : EntityDto<int>
    {
        public string FieldName { get; set; }
        public string Operator { get; set; }
        public string RuleName { get; set; }
        public string SeverityName { get; set; }
        public decimal ThresholdValue { get; set; }
        public string Description { get; set; }
    }
}
