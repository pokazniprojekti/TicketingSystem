using System.Collections.Generic;
using System.Web.Mvc;
using BugTracking.Models;

namespace BugTracking.Business
{
    public interface ICategoryService
    {
        BugTrackingResponse<Category> Save(Category category);
        List<Category> GetCategories();
        List<SelectListItem> SectionList();
        Category DeleteCategory(long Id);
        BugTrackingResponse<Category> DeleteConfirmed(int Id);
        Category EditCategory(long Id);
        BugTrackingResponse<Category> SaveEdit(Category category);
    }
}
