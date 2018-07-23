using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.IDAL;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.Shared;

namespace EntityFrameworkDemo.DAL
{
    public class CvDAL : ICvDAL, IDisposable
    {
        private readonly DemoDbContext _dbContext;
        private readonly LogAdapter    _logger;
        private          UserInfo      _userInfo;

        public CvDAL(DemoDbContext dbContext,
                         LogAdapter    logger,
                         UserInfo      userInfo)
        {
            _dbContext = dbContext;
            _logger    = logger;
            _userInfo  = userInfo;
            _logger.Initial<CountyDAL>();
        }

        public IEnumerable<CompCv> Get()
        {
            return _dbContext.CompCv
                             .Include(cv => cv.CompCvCertificates)
                             .Include(cv => cv.CompCvEducations)
                             .Include(cv => cv.CompCvLanguageRequirements)
                             .Include(cv => cv.Country)
                             .Include(cv => cv.Country.CountryLanguages)
                             .Include(cv => cv.County)
                             .Include(cv => cv.County.CountyLanguages)
                             .AsNoTracking();
        }

        public CompCv Get(Guid id)
        {
            return _dbContext.CompCv
                             .Include(cv => cv.CompCvCertificates)
                             .Include(cv => cv.CompCvEducations)
                             .Include(cv => cv.CompCvLanguageRequirements)
                             .Include(cv => cv.Country)
                             .Include(cv => cv.Country.CountryLanguages)
                             .Include(cv => cv.County)
                             .Include(cv => cv.County.CountyLanguages)
                             .AsNoTracking()
                             .FirstOrDefault(c => c.CvId == id);
        }

        public IEnumerable<CountryLanguage> GetCountryIdNames(string currentLanguage)
        {
            return _dbContext.CountryLanguage
                             .Where(c => c.Language == currentLanguage)
                             .AsNoTracking();
        }

        public IEnumerable<CountyLanguage> GetCountyIdNames(string currentLanguage)
        {
            return _dbContext.CountyLanguage
                             .Where(c => c.Language == currentLanguage)
                             .AsNoTracking();
        }

        public void Add(CompCv entity)
        {
            _dbContext.CompCv.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}