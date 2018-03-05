using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EntityModel = DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
using BugTracking.Business.Validation.Specifications;

namespace BugTracking.Business
{
    public class PriorityService : BusinessServiceBase, IPriorityService
    {
        private static readonly string ClassName = typeof(PriorityService).FullName;

        public BugTrackingResponse<BusinessObjects.Priority> DeleteConfirmed(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeletePriority");

            var response = new BugTrackingResponse<BusinessObjects.Priority>();


            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var query = db.Priorities.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                        var prior = MapperAllLevels.Map<EntityModel.Priority, BusinessObjects.Priority>(query);
                        var validator = new PriorityDeleteSpecification();
                        var result = validator.Validate(prior);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            //db.Priorities.Remove(query);
                            query.Active = false;
                            query.LastModified = DateTime.Now;
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
                        Logger.TraceMethodEnd(ClassName, "DeletePriority");
                    }

                }
            }
        }

        public BusinessObjects.Priority DeletePriority(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeletePriority");

            var prior = default(BusinessObjects.Priority);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Priorities.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                    prior = MapperAllLevels.Map<EntityModel.Priority, BusinessObjects.Priority>(query);
                    return prior;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeletePriority");
            }
        }

        public BusinessObjects.Priority EditPriority(int Id)
        {
            Logger.TraceMethodStart(ClassName, "EditPriority");

            var prior = default(BusinessObjects.Priority);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Priorities.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                    prior = MapperAllLevels.Map<EntityModel.Priority, BusinessObjects.Priority>(query);
                    return prior;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditPriority");
            }
        }


        public List<BusinessObjects.Priority> GetPriorities()
        {
            Logger.TraceMethodStart(ClassName, "GetPriorities");
            var priorities = default(List<BusinessObjects.Priority>);

            try
            {

                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Priorities.Where(c=>c.Active).ToList();
                    priorities = MapperAllLevels.Map<List<EntityModel.Priority>, List<BusinessObjects.Priority>>(query);
                    return priorities;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetPriorities");
            }
        }

        public BugTrackingResponse<BusinessObjects.Priority> Save(BusinessObjects.Priority prio)
        {
            Logger.TraceMethodStart(ClassName, "Save");

            var response = new BugTrackingResponse<BusinessObjects.Priority>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var validator = new PrioritySpecification();
                        var result = validator.Validate(prio);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            prio.LastModified = DateTime.Now;
                            prio.Active = true;
                            var newprio = MapperAllLevels.Map<BusinessObjects.Priority, EntityModel.Priority>(prio);
                            db.Priorities.Add(newprio);
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
                        Logger.TraceMethodEnd(ClassName, "Save");
                    }

                }
            }
        }

        public BugTrackingResponse<BusinessObjects.Priority> SaveEdit(BusinessObjects.Priority prio)
        {
            Logger.TraceMethodStart(ClassName, "SaveEdit");

            var response = new BugTrackingResponse<BusinessObjects.Priority>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var validator = new PrioritySpecification();
                        var result = validator.Validate(prio);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {

                            prio.LastModified = DateTime.Now;
                            var newprior = MapperAllLevels.Map<BusinessObjects.Priority, EntityModel.Priority>(prio);
                            db.Entry(newprior).State = EntityState.Modified;
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

        public List<BusinessObjects.Priority> DropDownPriority()
        {
            Logger.TraceMethodStart(ClassName);


            var dropdown = default(List<BusinessObjects.Priority>);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var Priorityquery = db.Priorities.Select(c => c).Where(d => d.Active).ToList();
                    dropdown = MapperAllLevels.Map<List<EntityModel.Priority>, List<BusinessObjects.Priority>>(Priorityquery);
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
                Logger.TraceMethodEnd(ClassName, "DropDownPriority");
            }

        }



    }
}
