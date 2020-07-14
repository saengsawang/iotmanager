using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace IoT.Core.Cities
{
    public class CityManager:DomainService,ICityManager
    {
        private readonly ICityRepository _cityRepositories;
        public CityManager(ICityRepository cityRepositories)
        {
            _cityRepositories = cityRepositories;
        }

        public void Delete(City entity)
        {
            _cityRepositories.AffiliateDelete(entity);
        }

        public City Update(City entity)
        {
           return _cityRepositories.Update(entity);
        }

        public IQueryable<City> GetAll()
        {
            return _cityRepositories.GetAll();
        }

        
    }
}
