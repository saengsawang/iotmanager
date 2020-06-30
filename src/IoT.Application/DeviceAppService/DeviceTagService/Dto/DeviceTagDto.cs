using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace IoT.Application.DeviceAppService
{
    public class DeviceTagDto:EntityDto<int>
    {

        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
