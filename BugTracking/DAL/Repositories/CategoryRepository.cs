using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EntityModel;

namespace DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BugTrackingEntities context) : base(context)
        {
        }

        public List<Category> GetActiveCategories(bool flag)
        {
            return BugTrackingEntities.Categories.Where(c => c.Active == flag).ToList();
        }

        public Category SelectCategory(long Id)
        {
            return BugTrackingEntities.Categories.Where(c => c.ID == Id && c.Active).FirstOrDefault();
        }

        public BugTrackingEntities BugTrackingEntities
        {
            get { return Context as BugTrackingEntities; }
        }
    }
}
