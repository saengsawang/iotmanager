using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.DeviceAppService.DeviceService.Dto
{
    public class UpdateDeviceDto : EntityDto<int>
    {
        public string DeviceName { get; set; }
        public string DeviceTypeName { get; set; }
        public string CityName { get; set; }
        public string FactoryName { get; set; }
        public string WorkshopName { get; set; }
        public string GatewayName { get; set; }
        public string Remark { get; set; }
        public string ImageUrl { get; set; }
        public string Base64Image { get; set; }
        public string PictureRoute { get; set; }
    }
}
