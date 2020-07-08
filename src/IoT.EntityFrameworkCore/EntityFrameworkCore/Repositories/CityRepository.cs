using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.EntityFrameworkCore;
using IoT.Core;
using IoT.Core.Cities;
using IoT.Core.Factories;
using Microsoft.AspNetCore.Http;

namespace IoT.EntityFrameworkCore.Repositories
{
    public class CityRepository:IoTRepositoryBase<City,int>,ICityRepository
    {
        private readonly IFactoryManager _factoryManager;
        public CityRepository(IFactoryManager factoryManager,IDbContextProvider<IoTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            _factoryManager = factoryManager;
        }

        public void AffiliateDelete(City entity)
        {
            var query = _factoryManager.GetAll().Where(f => f.CityId == entity.Id);
            ArrayList list = new ArrayList(query.Count());
            if (query.Any())
            {
                foreach (var factory in query)
                {
                    list.Add((Factory)factory);
                }
            }
            foreach(var factory in list)
            {
                _factoryManager.Delete((Factory)factory);
            }
            Delete(entity);
        }

        public City Create(City entity)
        {
            var cities = GetAll().Where(c => c.CityName == entity.CityName || c.CityCode == entity.CityCode);
            if (!cities.Any())
            {
                return Insert(entity);
            }
            else
            {
                throw new ApplicationException($"城市：{entity.CityName} 已存在！");
            }

            
        }
    }
}
