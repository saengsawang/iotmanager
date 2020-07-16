using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IoT.Application.FieldAppService.DTO;
using L._52ABP.Application.Dtos;

namespace IoT.Application.FieldAppService
{
    public interface IFieldAppService : ICrudAppService<FieldDto, int, PagedSortedAndFilteredInputDto, FieldDto>
    {
        void BatchDelete(int[] inputs);
        long GetNumber();
        PagedResultDto<FieldDto> GetByDevice(string DeviceName);
        FieldDto GetByName(string fieldName);
    }
}
