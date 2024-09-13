using BS.DMO.ViewModels.HRMS.Setup;

namespace BS.Infra.Services.HRMS.Setup
{
    public class DesignationService
    {
        private readonly AppDbContext dbCtx;
        public DesignationService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(DESIGNATION obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "DESIGNATION";
            try
            {
                if (obj.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.ID = Guid.NewGuid().ToString();

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.DESIGNATION.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.DESIGNATION.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.SENIOR_LEVEL = obj.SENIOR_LEVEL;
                            entity.SHORT_NAME = obj.SHORT_NAME;
                            entity.DESIGNATION_NAME = obj.DESIGNATION_NAME;
                            entity.PARENT_ID = obj.PARENT_ID;
                            entity.LOWER_BOUND = obj.LOWER_BOUND;
                            entity.UPPER_BOUND = obj.UPPER_BOUND;
                            //Start Audit
                            entity.IS_ACTIVE = obj.IS_ACTIVE;
                            entity.UPDATE_USER = userId;
                            entity.UPDATE_DATE = DateTime.Now;
                            entity.REVISE_NO = entity.REVISE_NO + 1;
                            //End Audit
                            dbCtx.Entry(entity).State = EntityState.Modified;
                            eQResult.rows = dbCtx.SaveChanges();
                            eQResult.success = true;
                            eQResult.messages = NotifyService.EditSuccess();
                            return eQResult;
                        }
                        else
                        {
                            eQResult.messages = NotifyService.EditRestricted();
                            return eQResult;
                        }
                    }
                    else
                    {
                        eQResult.messages = NotifyService.NotFound();
                        return eQResult;
                    }
                }
            }
            catch (Exception ex)
            {
                eQResult.messages = NotifyService.Error(ex.Message == string.Empty ? ex.InnerException.Message : ex.Message);
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }

        public List<DESIGNATION_VM> GetAll()
        {
            FormattableString sql = $@"select d.*,pd.DESIGNATION_NAME PARENT_NAME
                                    from DESIGNATION d
                                    Left Join DESIGNATION pd on d.PARENT_ID = pd.ID ORDER BY D.SENIOR_LEVEL";
            return dbCtx.Database.SqlQuery<DESIGNATION_VM>(sql).ToList();
        }
        public List<DESIGNATION> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM DESIGNATION BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.SENIOR_LEVEL DESC";
            return dbCtx.Database.SqlQuery<DESIGNATION>(sql).ToList();
        }
        public DESIGNATION GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM DESIGNATION BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<DESIGNATION>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "DESIGNATION";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                ////check child entity
                //int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.DESIGNATION.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.DESIGNATION.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.SHORT_NAME!);
                    return eQResult;
                }
                else
                {
                    eQResult.messages = NotifyService.NotFoundString();
                    return eQResult;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message == string.Empty ? ex.InnerException.Message : ex.Message;
                eQResult.messages = msg.Replace("'", "");
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
    }
}
