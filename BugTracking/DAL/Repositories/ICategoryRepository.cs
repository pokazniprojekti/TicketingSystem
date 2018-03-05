using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EntityModel;

namespace DAL.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetActiveCategories(bool flag);
        Category SelectCategory(long Id);
    }
}
