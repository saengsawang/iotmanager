using System;
namespace IoT.Application.FieldAppService.DTO
{
    public class CreateFieldDto
    {
        public string FieldName { get; set; }
        public string DeviceName { get; set; }
        public string IndexId { get; set; }
    }
}
