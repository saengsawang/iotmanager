using System;
using Abp.Application.Services;
using IoT.Application.FieldAppService.DTO;
using L._52ABP.Application.Dtos;

namespace IoT.Application.FieldAppService
{
    public interface IFieldAppService : ICrudAppService<FieldDto, int, PagedSortedAndFilteredInputDto, FieldDto>
    {
    }
}
