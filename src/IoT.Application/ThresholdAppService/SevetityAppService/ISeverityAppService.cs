using System;
using Abp.Application.Services;
using IoT.Application.ThresholdAppService.SevetityAppService.DTO;
using L._52ABP.Application.Dtos;

namespace IoT.Application.ThresholdAppService.SevetityAppService
{
    public interface ISeverityAppService : ICrudAppService<SeverityDto, int, PagedSortedAndFilteredInputDto, CreateSeverityDto>
    {
    }
}
