using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.BLL.IBLL
{
    public interface ICountyBLL
    {
        UserInfo UserInfo { set; }

        List<CountyVM>               Get();
        CountyVM                     Get(Guid        id);
        bool                         Add(CountyVM    countyVm);
        bool                         Update(CountyVM countyVm);
        bool                         Del(Guid        id);
        IEnumerable<CountryLanguage> GetIdAndCurrentLanguageNames();
    }
}