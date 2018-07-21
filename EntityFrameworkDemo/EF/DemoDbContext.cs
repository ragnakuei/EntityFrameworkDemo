using System.Data.Entity;
using EntityFrameworkDemo.Models.EntityModel;
using NLog;

namespace EntityFrameworkDemo.EF
{
    public class DemoDbContext : DbContext
    {
        private readonly ILogger _logger;

        private DemoDbContext() : base("name=DemoDbContext")
        {
            _logger      = LogManager.GetCurrentClassLogger();
            Database.Log = log => _logger.Debug(log);
        }

        public static DemoDbContext Create()
        {
            var dbContext = new DemoDbContext();

            // reference: https://dotblogs.com.tw/yc421206/2015/05/03/151200#%E5%81%9C%E7%94%A8EF%E5%85%A7%E5%BB%BA%E6%AA%A2%E6%9F%A5
            dbContext.Configuration.ProxyCreationEnabled     = false;
            dbContext.Configuration.LazyLoadingEnabled       = false;
            dbContext.Configuration.AutoDetectChangesEnabled = false;
            return dbContext;
        }

        public DbSet<CompCv>                    CompCv                    { get; set; }
        public DbSet<CompCvCertificate>         CompCvCertificate         { get; set; }
        public DbSet<CompCvEducation>           CompCvEducation           { get; set; }
        public DbSet<CompCvLanguageRequirement> CompCvLanguageRequirement { get; set; }
        public DbSet<Country>                   Country                   { get; set; }
        public DbSet<CountryLanguage>           CountryLanguage           { get; set; }
        public DbSet<County>                    County                    { get; set; }

        public DbSet<CountyLanguage> CountyLanguage { get; set; }
        //public DbSet<ActionReceipt>             ActionReceipt             { get; set; }
        //public DbSet<ActionReceiptNoteLanguage> ActionReceiptNoteLanguage { get; set; }
    }
}