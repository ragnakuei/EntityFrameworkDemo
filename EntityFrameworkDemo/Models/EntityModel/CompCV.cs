﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("CompCv")]
    public class CompCv
    {
        public CompCv()
        {
            this.CompCvCertificates         = new List<CompCvCertificate>();
            this.CompCvEducations           = new List<CompCvEducation>();
            this.CompCvLanguageRequirements = new List<CompCvLanguageRequirement>();
        }

        [Key]
        public Guid CvId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public Guid CountryId { get;  set; }
        public Guid CountyId  { get;  set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        [ForeignKey("CountyId")]
        public County County { get;                                              set; }
        public List<CompCvCertificate>         CompCvCertificates         { get; set; }
        public List<CompCvEducation>           CompCvEducations           { get; set; }
        public List<CompCvLanguageRequirement> CompCvLanguageRequirements { get; set; }
    }
}