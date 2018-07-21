using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.Shared;

namespace EntityFrameworkDemo.DAL.IDAL
{
    public interface ICountryDAL
    {
        UserInfo UserInfo { set; }

        IEnumerable<Country>         Get();
        bool                         Add(Country    country);
        Country                      Get(Guid       id);
        bool                         Update(Country entity);
        bool                         Delete(Guid    id);
        IEnumerable<CountryLanguage> GetIdAndCurrentLanguageNames();
    }
}