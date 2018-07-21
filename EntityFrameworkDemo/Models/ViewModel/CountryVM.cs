using System;

namespace EntityFrameworkDemo.Models.ViewModel
{
    public class CountryVM
    {
        public Guid   Id       { get; set; }
        public string Code { get; set; }

        public Guid LanguageId { get; set; }
        public string Language { get; set; }
        public string Name     { get; set; }
    }
}