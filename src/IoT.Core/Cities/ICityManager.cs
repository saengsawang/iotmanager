using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Domain.Services;

namespace IoT.Core.Cities
{
    public interface ICityManager:IDomainService
    {
        void Delete(City entity);
        IQueryable<City> GetAll();
    }
}
