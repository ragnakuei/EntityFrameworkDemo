using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.EntityModel;

namespace EntityFrameworkDemo.DAL
{
    public interface ICountryDAL
    {
        IEnumerable<Country> Get();
        bool Add(Country country);
        Country Get(Guid id);
    }

    public class CountryDAL : ICountryDAL
    {
        private readonly DemoDbContext _dbContext;
        private readonly LogAdapter    _logger;

        public CountryDAL(DemoDbContext dbContext, LogAdapter logger)
        {
            _dbContext = dbContext;
            _logger    = logger;
            _logger.Initial<CountryDAL>();
        }

        public IEnumerable<Country> Get()
        {
            return _dbContext.Country
                             .AsNoTracking()
                             .Include("CountryLanguages");
        }

        public Country Get(Guid id)
        {
            var country = _dbContext.Country.Find(id);
            var countryLanguage = _dbContext.ConCountryLanguages
                                            .AsNoTracking()
                                            .Where(l => l.CountryId == country.CountryId).ToList();
            country.CountryLanguages = countryLanguage;
            return country;
        }

        public bool Add(Country country)
        {
            try
            {
                _dbContext.Country.Add(country);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                _logger.Error(e.Message);
                return false;
            }
        }
    }
}