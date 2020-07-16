using System;
using Abp.Application.Services;
using IoT.Application.ThresholdAppService.DTO;
using L._52ABP.Application.Dtos;

namespace IoT.Application.ThresholdAppService
{
    public interface IThresholdAppService : ICrudAppService<ThresholdDto, int, PagedSortedAndFilteredInputDto, ThresholdDto>
    {
        ThresholdDto GetByName(string ruleName);
    }
}
