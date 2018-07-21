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
    public class CountryBLL : ICountryBLL
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
            result.Id   = entity.CountryId;
            result.Code = entity.Code;

            var currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();

            var countryLanguage = entity.CountryLanguages
                                        .FirstOrDefault(l => l.Language == currentLanguage);
            if (countryLanguage == null)
            {
                result.Language = Thread.CurrentThread.CurrentCulture.ToString();
                result.LanguageId = Guid.NewGuid();
            }
            else
            {
                result.Language   = currentLanguage;
                result.LanguageId = countryLanguage.CountryLanguageId;
                result.Name       = countryLanguage.Name;
            }

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
                                              Language          = Thread.CurrentThread.CurrentCulture.ToString(),
                                              Name              = countryVm.Name,
                                              //CountryId         = entity.CountryId   // 可以不用預先給定
                                          }
                                      };
            return entity;
        }

        public bool Update(CountryVM countryVm)
        {
            var result = new Country
                         {
                             CountryId        = countryVm.Id,
                             Code             = countryVm.Code,
                             CountryLanguages = new List<CountryLanguage>()
                         };

            var item = new CountryLanguage();
            item.CountryId = countryVm.Id;
            item.Name = countryVm.Name;
            item.Language = countryVm.Language ?? Thread.CurrentThread.CurrentCulture.ToString();
            item.CountryLanguageId = countryVm.LanguageId.HasValue 
                                         ? countryVm.LanguageId.Value 
                                         : Guid.NewGuid();
            result.CountryLanguages.Add(item);
            return _dal.Update(result);
        }

        public bool Del(Guid id)
        {
            return _dal.Delete(id);
        }
    }
}