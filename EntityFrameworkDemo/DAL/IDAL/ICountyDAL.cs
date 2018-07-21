using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.Shared;

namespace EntityFrameworkDemo.DAL.IDAL
{
    public interface ICountyDAL
    {
        IEnumerable<County> Get();
        County              Get(Guid      id);
        bool                Add(County    entity);
        bool                Update(County result);
        bool                Delete(Guid   id);
    }
}