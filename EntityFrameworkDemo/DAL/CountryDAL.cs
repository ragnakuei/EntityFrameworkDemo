﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.EntityModel;

namespace EntityFrameworkDemo.DAL
{
    public class CountryDAL : ICountryDAL, IDisposable
    {
        private readonly DemoDbContext _dbContext;
        private readonly LogAdapter _logger;

        public CountryDAL(DemoDbContext dbContext, LogAdapter logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _logger.Initial<CountryDAL>();
        }

        public IEnumerable<Country> Get()
        {
            // 使用 Left Join
            return _dbContext.Country
                             .Include(c => c.CountryLanguages)
                             .AsNoTracking();
        }

        public Country Get(Guid id)
        {
            // 分開查詢
            var country = _dbContext.Country
                                    .AsNoTracking()
                                    .FirstOrDefault(c => c.CountryId == id);

            var currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();
            var countryLanguage = _dbContext.CountryLanguage
                                            .Where(l => l.CountryId == id
                                                        && l.Language == currentLanguage)
                                            .AsNoTracking();
            country.CountryLanguages = countryLanguage.ToList();
            return country;
        }

        public bool Add(Country country)
        {
            try
            {
                _dbContext.Country.Add(country);
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                _logger.Error(e.Message);
                return false;
            }
        }

        /// <summary>
        ///     想法：從資料庫上抓出完整的資料，透過 attach 加上狀態，透過修改 attach entity 的值來進行更新資料 ~!!
        /// </summary>
        public bool Update(Country updateEntity)
        {
            if (updateEntity == null)
                throw new Exception("Country 無對應資料可更新");

            var countryInDB = _dbContext.Country.First(c => c.CountryId == updateEntity.CountryId);
            if (countryInDB == null)
                throw new Exception("Country 無對應資料可更新");

            var attachedCountry = _dbContext.Country.Attach(countryInDB);
            attachedCountry.Code = updateEntity.Code;

            _dbContext.Entry(attachedCountry).Property(p => p.Code).IsModified = true;

            _dbContext.CountryLanguage.AddOrUpdate(updateEntity.CountryLanguages.First());

            // 同時更新二個 Table，會自動加上 transaction
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {  
            var delCountry = _dbContext.Country.FirstOrDefault(c => c.CountryId == id);
            var delCountryLanguages = _dbContext.CountryLanguage.Where(c => c.CountryId == delCountry.CountryId);

            _dbContext.CountryLanguage.RemoveRange(delCountryLanguages);
            _dbContext.Country.Remove(delCountry);

            return _dbContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}