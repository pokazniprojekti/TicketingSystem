using EntityModel = DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
using BugTracking.Business.Validation.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using DAL.UnitOfWork;
using DAL.EntityModel;

namespace BugTracking.Business
{
    public class CategoryService : BusinessServiceBase, ICategoryService
    {
        UnitOfWork UoW = new UnitOfWork(new BugTrackingEntities());
        private static readonly string ClassName = typeof(CategoryService).FullName;

        public List<BusinessObjects.Category> GetCategories()
        {
            Logger.TraceMethodStart(ClassName, "GetCategories");
            var categories = default(List<BusinessObjects.Category>);

            try
            {
                var query = UoW.Categories.GetActiveCategories(true);
                categories = MapperAllLevels.Map<List<EntityModel.Category>, List<BusinessObjects.Category>>(query);
                return categories;
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetCategories");
            }
        }

        public BugTrackingResponse<BusinessObjects.Category> Save(BusinessObjects.Category category)
        {
            Logger.TraceMethodStart(ClassName, "Save");

            var response = new BugTrackingResponse<BusinessObjects.Category>();

            try
            {
                var validator = new CategorySpecification();
                var result = validator.Validate(category);
                var failures = result.Errors;

                response.ValidationResult = result;

                if (result.IsValid)
                {
                    category.LastModified = DateTime.Now;
                    category.Active = true;
                    var newcategory = MapperAllLevels.Map<BusinessObjects.Category, EntityModel.Category>(category);
                    UoW.Categories.Add(newcategory);
                    UoW.Complete();
                }
                else
                {
                    Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }
            finally
            {
                Logger.TraceMethodEnd(ClassName, "Save");
            }

        }

        public List<SelectListItem> SectionList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Technical", Value = "Technical" });
            items.Add(new SelectListItem { Text = "Sales", Value = "Sales" });
            return items;
        }

        public BugTrackingResponse<BusinessObjects.Category> SaveEdit(BusinessObjects.Category category)
        {
            Logger.TraceMethodStart(ClassName, "SaveEdit");

            var response = new BugTrackingResponse<BusinessObjects.Category>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new CategoryUpdateSpecification();
                        var result = validator.Validate(category);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            category.LastModified = DateTime.Now;
                            var newcat = MapperAllLevels.Map<BusinessObjects.Category, EntityModel.Category>(category);
                            db.Entry(newcat).State = EntityState.Modified;

                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;
                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveEdit");
                    }

                }
            }
        }


        public BusinessObjects.Category DeleteCategory(long Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteCategory");
            var category = default(BusinessObjects.Category);

            try
            {
                var query = UoW.Categories.SelectCategory(Id);
                category = MapperAllLevels.Map<EntityModel.Category, BusinessObjects.Category>(query);
                return category;
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }
            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteCategory");
            }
        }



        public BugTrackingResponse<BusinessObjects.Category> DeleteConfirmed(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteCategory");

            var response = new BugTrackingResponse<BusinessObjects.Category>();

            try
            {
                var query = UoW.Categories.SelectCategory(Id);
                var orgdelete = MapperAllLevels.Map<EntityModel.Category, BusinessObjects.Category>(query);
                var validator = new CategoryDeleteSpecification();
                var result = validator.Validate(orgdelete);
                var failures = result.Errors;
                response.ValidationResult = result;

                if (result.IsValid)
                {

                    query.Active = false;
                    query.LastModified = DateTime.Now;
                    //db.Categories.Remove(query);
                    UoW.Complete();
                }
                else
                {
                    Logger.TraceErrorFormat("Error while DeleteOrganization {0}", response.ValidationResult.Errors);
                }
                return response;
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }
            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteCategory");
            }
        }

        public BusinessObjects.Category EditCategory(long Id)
        {
            Logger.TraceMethodStart(ClassName, "EditCategory");

            var resp = default(BusinessObjects.Category);

            try
            {
                var query = UoW.Categories.SelectCategory(Id);
                resp = MapperAllLevels.Map<EntityModel.Category, BusinessObjects.Category>(query);
                return resp;
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }
            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditResponsible");
            }
        }

        public List<BusinessObjects.Category> DropDownCategory()
        {
            Logger.TraceMethodStart(ClassName);


            var dropdown = default(List<BusinessObjects.Category>);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var Categoryquery = db.Categories.Select(c => c).Where(d => d.Active).ToList();
                    dropdown = MapperAllLevels.Map<List<EntityModel.Category>, List<BusinessObjects.Category>>(Categoryquery);
                    return dropdown;
                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DropDownCategory");
            }

        }
    }
}
