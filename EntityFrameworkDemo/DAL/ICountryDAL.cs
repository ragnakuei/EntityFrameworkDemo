using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.DAL
{
    public interface ICountryDAL
    {
        IEnumerable<Country> Get();
        bool Add(Country country);
        Country Get(Guid id);
        bool Update(CountryVM entity);
        bool Delete(Guid id);
    }
}