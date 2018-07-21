using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.BLL.IBLL
{
    public interface ICountryBLL
    {
        UserInfo        UserInfo { set; }

        List<CountryVM> Get();
        CountryVM       Get(Guid         id);
        bool            Add(CountryVM    countryVm);
        bool            Update(CountryVM countryVm);
        bool            Del(Guid         id);
    }
}