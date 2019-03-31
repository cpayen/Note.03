using Note.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Note.Core.Data
{
    public interface IEntityFactory
    {
        IRepository<T> GetRepository<T>() where T : Entity;
        Task SaveAsync();
    }
}
