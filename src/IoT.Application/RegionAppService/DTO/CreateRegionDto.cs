using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.RegionAppService.DTO
{
    public class CreateRegionDto : EntityDto<int>
    {
        public string Level { get; set; }
        public string RegionName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
