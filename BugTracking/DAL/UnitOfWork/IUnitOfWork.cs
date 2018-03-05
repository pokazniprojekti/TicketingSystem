using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        int Complete();
    }
}
