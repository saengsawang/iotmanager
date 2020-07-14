using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IoT.Application.CityAppService.DTO;
using IoT.Application.DeviceAppService;
using IoT.Application.DeviceAppService.DeviceService.Dto;
using IoT.Application.DeviceAppService.DeviceTagService.Dto;
using IoT.Application.DeviceAppService.DeviceTypeService.DTO;
using IoT.Application.DeviceAppService.OnlineTimeDailyService.Dto;
using IoT.Application.FactoryAppService.DTO;
using IoT.Application.FieldAppService.DTO;
using IoT.Application.GatewayAppService.DTO;
using IoT.Application.RegionAppService.DTO;
using IoT.Application.ThresholdAppService.DTO;
using IoT.Application.ThresholdAppService.SevetityAppService.DTO;
using IoT.Application.WorkshopAppService.DTO;
using IoT.Core;
using IoT.Core.Regions.Entity;

namespace IoT.Application
{
    public class IoTDtoProfile:Profile
    {
        public IoTDtoProfile()
        {
            //City
            CreateMap<City, CityDto>();
            CreateMap<CreateCityDto, City>();
            CreateMap<UpdateCityDto, City>();
            //Factory
            CreateMap<Factory, FactoryDto>()
                .ForMember(des => des.CityName,
                    opt => opt.MapFrom(i => i.City.CityName));
            CreateMap<CreateFactoryDto, Factory>();
            //Workshop
            CreateMap<Workshop, WorkshopDto>()
                .ForMember(des => des.FactoryName,
                    opt => opt.MapFrom(i => i.Factory.FactoryName))
                .ForMember(des=>des.CityName,
                    opt=>opt.MapFrom(i=>i.Factory.City.CityName));
            CreateMap<CreateWorkshopDto, Workshop>();
            //GatewayType
            CreateMap<GatewayType, GatewayTypeDto>();
            CreateMap<CreateGatewayTypeDto, GatewayType>();
            //Gateway
            CreateMap<Gateway, GatewayDto>()
                .ForMember(des => des.WorkshopName,
                    opt => opt.MapFrom(i => i.Workshop.WorkshopName))
                .ForMember(des => des.FactoryName,
                    opt => opt.MapFrom(i => i.Workshop.Factory.FactoryName))
                .ForMember(des => des.CityName,
                    opt => opt.MapFrom(i => i.Workshop.Factory.City.CityName))
                .ForMember(des=>des.GatewayTypeName,
                    opt=>opt.MapFrom(i=>i.GatewayType.TypeName));
            CreateMap<CreateGatewayDto, Gateway>();
            //Tag
            CreateMap<Tag, TagDto>();
            CreateMap<CreateTagDto, Tag>();
            //DeviceType
            CreateMap<DeviceType, DeviceTypeDto>();
            CreateMap<CreateDeviceTypeDto, DeviceType>();
            //Device
            CreateMap<Device, DeviceDto>()
            .ForMember(des => des.GatewayName,
            opt => opt.MapFrom(i => i.Gateway.GatewayName))
            .ForMember(des => des.WorkshopName,
                    opt => opt.MapFrom(i => i.Gateway.Workshop.WorkshopName))
            .ForMember(des => des.FactoryName,
                    opt => opt.MapFrom(i => i.Gateway.Workshop.Factory.FactoryName))
            .ForMember(des => des.CityName,
                    opt => opt.MapFrom(i => i.Gateway.Workshop.Factory.City.CityName))
            .ForMember(des => des.DeviceTypeName,
            opt => opt.MapFrom(i => i.DeviceType.TypeName));
            CreateMap<CreateDeviceDto, Device>();
            //DeviceTag
            CreateMap<DeviceTag,DeviceTagDto>()
            .ForMember(des => des.DeviceName,
            opt => opt.MapFrom(i=>i.Device.DeviceName));
            CreateMap<CreateDeviceTagDto,Device>();
            //OnlineTimeDaily
            CreateMap<OnlineTimeDaily, OnlineTimeDailyDto>()
                .ForMember(des => des.DeviceName,
                opt => opt.MapFrom(i => i.Device.DeviceName));
            CreateMap<CreateOnlineTimeDailyDto, OnlineTimeDaily>();
            //Field
            CreateMap<Field, FieldDto>()
                .ForMember(des => des.DeviceName,
                opt => opt.MapFrom(i => i.Device.DeviceName));
            CreateMap<CreateFieldDto, Field>();
            //Threshold
            CreateMap<Threshold, ThresholdDto>()
                .ForMember(des => des.FieldName,
                opt=>opt.MapFrom(i=>i.Field.FieldName))
                .ForMember(des=>des.SeverityName,
                opt => opt.MapFrom(i=>i.Severity.SeverityName));
            CreateMap<CreateThresholdDto, Threshold>();
            //Severity
            CreateMap<Severity, SeverityDto>();
            CreateMap<CreateSeverityDto, Severity>();
            //Region
            CreateMap<Region, RegionDto>();
            CreateMap<CreateRegionDto, Region>();
        }
    }
}
