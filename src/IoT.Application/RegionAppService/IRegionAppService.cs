using System;
using Abp.Application.Services;
using IoT.Application.RegionAppService.DTO;
using L._52ABP.Application.Dtos;

namespace IoT.Application.RegionAppService
{
    public interface IRegionAppService : ICrudAppService<RegionDto, int, PagedSortedAndFilteredInputDto, CreateRegionDto>
    {
    }
}
