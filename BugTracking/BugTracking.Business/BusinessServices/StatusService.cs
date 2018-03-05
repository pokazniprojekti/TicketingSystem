using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BusinessObjects = BugTracking.Models;
using EntityModel = DAL.EntityModel;
using BugTracking.Business.Validation.Specifications;

namespace BugTracking.Business
{
    public class StatusService : BusinessServiceBase, IStatusService
    {
        private static readonly string ClassName = typeof(StatusService).FullName;

        public BugTrackingResponse<BusinessObjects.Status> DeleteConfirmed(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteStatus");

            var response = new BugTrackingResponse<BusinessObjects.Status>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var query = db.Status.Where(c => c.ID == Id && c.Active == true).FirstOrDefault();
                        var status = MapperAllLevels.Map<EntityModel.Status, BusinessObjects.Status>(query);
                        var validator = new StatusDeleteSpecification();
                        var result = validator.Validate(status);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            
                            //db.Status.Remove(query);
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
                        Logger.TraceMethodEnd(ClassName, "DeleteConfirmed");
                    }

                }
            }
        }

        public BusinessObjects.Status DeleteStatus(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteStatus");

            var status = default(BusinessObjects.Status);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Status.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                    status = MapperAllLevels.Map<EntityModel.Status, BusinessObjects.Status>(query);
                    return status;

                }

            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteStatus");
            }
        }

        public BusinessObjects.Status EditStatus(int Id)
        {
            Logger.TraceMethodStart(ClassName, "EditStatus");

            var status = default(BusinessObjects.Status);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Status.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                    status = MapperAllLevels.Map<EntityModel.Status, BusinessObjects.Status>(query);
                    return status;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditStatus");
            }

        }

        public List<BusinessObjects.Status> GetStatus()
        {
            Logger.TraceMethodStart(ClassName, "GetStatus");

            var status = default(List<BusinessObjects.Status>);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Status.Where(c=>c.Active).ToList();
                    status = MapperAllLevels.Map<List<EntityModel.Status>, List<BusinessObjects.Status>>(query);
                    return status;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetStatus");
            }
        }

        public BugTrackingResponse<BusinessObjects.Status> Save(BusinessObjects.Status stat)
        {
            Logger.TraceMethodStart(ClassName, "Save");

            var response = new BugTrackingResponse<BusinessObjects.Status>();


            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var validator = new StatusSpecification();
                        var result = validator.Validate(stat);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            stat.Active = true;
                            stat.LastModified = DateTime.Now;
                            var newstat = MapperAllLevels.Map<BusinessObjects.Status, EntityModel.Status>(stat);
                            db.Status.Add(newstat);
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

        public BugTrackingResponse<BusinessObjects.Status> SaveEdit(BusinessObjects.Status stat)
        {
            Logger.TraceMethodStart(ClassName, "SaveEdit");

            var response = new BugTrackingResponse<BusinessObjects.Status>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var validator = new StatusUpdateSpecification();
                        var result = validator.Validate(stat);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            stat.LastModified = DateTime.Now;
                            var newstatus = MapperAllLevels.Map<BusinessObjects.Status, EntityModel.Status>(stat);
                            db.Entry(newstatus).State = EntityState.Modified;

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
        public List<BusinessObjects.Status> DropDownStatus()
        {
            Logger.TraceMethodStart(ClassName);


            var dropdown = default(List<BusinessObjects.Status>);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var Statusquery = db.Status.Select(c => c).Where(d => d.Active).ToList();
                    dropdown = MapperAllLevels.Map<List<EntityModel.Status>, List<BusinessObjects.Status>>(Statusquery);
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
                Logger.TraceMethodEnd(ClassName, "DropDownStatus");
            }

        }

    }
}
