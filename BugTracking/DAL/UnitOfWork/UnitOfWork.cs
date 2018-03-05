using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EntityModel;
using DAL.Repositories;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BugTrackingEntities _context;

        public UnitOfWork(BugTrackingEntities context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
        }

        public ICategoryRepository Categories { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
