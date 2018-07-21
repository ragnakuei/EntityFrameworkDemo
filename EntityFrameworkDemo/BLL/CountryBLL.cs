using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EntityFrameworkDemo.DAL;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.BLL
{
    public class CountryBLL : ICountryBLL, IDisposable
    {
        private readonly ICountryDAL _dal;
        private readonly LogAdapter  _logger;

        public CountryBLL(ICountryDAL dal, LogAdapter logger)
        {
            _dal    = dal;
            _logger = logger;
            _logger.Initial(nameof(CountryBLL));
        }

        public List<CountryVM> Get()
        {
            var entities = _dal.Get()
                               .Select(c => ToCountryVM(c))
                               .ToList();
            return entities;
        }

        private CountryVM ToCountryVM(Country entity)
        {
            var result = new CountryVM();
            result.Id = entity.CountryId;
            result.Code = entity.Code;

            var currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();

            var countryLanguage = entity.CountryLanguages
                                        .FirstOrDefault(l => l.Language == currentLanguage);
            if (countryLanguage == null)
                throw new Exception("指定 Country 無對應語系資料");

            result.Language = currentLanguage;
            result.LanguageId = countryLanguage.CountryLanguageId;
            result.Name     = countryLanguage.Name;
            
            return result;
        }

        public CountryVM Get(Guid id)
        {
            var entity = _dal.Get(id);
            var result = ToCountryVM(entity);
            return result;
        }

        public bool Add(CountryVM countryVm)
        {
            var entity = new Country();
            entity = ToCountryInsertEntity(countryVm);
            return _dal.Add(entity);
        }

        private static Country ToCountryInsertEntity(CountryVM countryVm)
        {
            Country entity;
            entity = new Country
                     {
                         CountryId = Guid.NewGuid(),
                         Code      = countryVm.Code
                     };
            entity.CountryLanguages = new List<CountryLanguage>
                                      {
                                          new CountryLanguage
                                          {
                                              CountryLanguageId = Guid.NewGuid(),
                                              Language          = countryVm.Language,
                                              Name              = countryVm.Name,
                                              //CountryId         = entity.CountryId   // 可以不用預先給定
                                          }
                                      };
            return entity;
        }

        public bool Update(CountryVM country)
        {
            return _dal.Update(country);
        }

        public bool Del(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}