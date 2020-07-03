using System;
using Abp.Application.Services.Dto;

namespace IoT.Application.FieldAppService.DTO
{
    public class FieldDto : EntityDto<int>
    {
        public string FieldName { get; set; }
        public string DeviceName { get; set; }
        public string IndexId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
