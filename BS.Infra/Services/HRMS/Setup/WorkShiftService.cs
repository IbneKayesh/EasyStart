using BS.DMO.Models.HRMS.Setup;

namespace BS.Infra.Services.HRMS.Setup
{
    public class WorkShiftService
    {
        private readonly AppDbContext dbCtx;
        public WorkShiftService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(WORK_SHIFT obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "WORK_SHIFT";
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

                    dbCtx.WORK_SHIFT.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.WORK_SHIFT.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.SHIFT_NAME = obj.SHIFT_NAME;
                            entity.IN_TIME_START = obj.IN_TIME_START;
                            entity.IN_TIME_END = obj.IN_TIME_END;
                            entity.OUT_TIME_START = obj.OUT_TIME_START;
                            entity.OUT_TIME_END = obj.OUT_TIME_END;
                            entity.GRACE_MINUTE = obj.GRACE_MINUTE;
                            entity.MAX_OT_HOUR = obj.MAX_OT_HOUR;
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

        public List<WORK_SHIFT> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM WORK_SHIFT BI
                    ORDER BY BI.SHIFT_NAME";
            return dbCtx.Database.SqlQuery<WORK_SHIFT>(sql).ToList();
        }
        public List<WORK_SHIFT> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM WORK_SHIFT BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.SHIFT_NAME";
            return dbCtx.Database.SqlQuery<WORK_SHIFT>(sql).ToList();
        }
        public WORK_SHIFT GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM WORK_SHIFT BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<WORK_SHIFT>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "WORK_SHIFT";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                if (anyChild > 0)
                {
                    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.WORK_SHIFT.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.WORK_SHIFT.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.SHIFT_NAME!);
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
