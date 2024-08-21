namespace BS.Infra.Services.HelpDesk
{
    public class WorkTaskService
    {
        private readonly AppDbContext dbCtx;
        public WorkTaskService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(WORK_TASK obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "WORK_TASK";
            try
            {
                if (obj.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.ID = Guid.NewGuid().ToString();
                    obj.REQUEST_DATE = dateTime;
                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = dateTime;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = dateTime;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.WORK_TASK.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.WORK_TASK.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.WT_TYPE = obj.WT_TYPE;
                            entity.PARENT_ID = obj.PARENT_ID;
                            entity.BG_ID = obj.BG_ID;
                            entity.STATUS_ID = obj.STATUS_ID;
                            entity.PRIORITY_ID = obj.PRIORITY_ID;
                            entity.TASK_TITLE = obj.TASK_TITLE;
                            entity.TASK_DESC = obj.TASK_DESC;
                            entity.TASK_FILE = obj.TASK_FILE;
                            entity.PROGRESS_PCT = obj.PROGRESS_PCT;
                            entity.TASK_VALUE = obj.TASK_VALUE;
                            entity.TAG_LIST = obj.TAG_LIST;
                            entity.REQUEST_USER = obj.REQUEST_USER;
                            entity.REQUEST_DATE = obj.REQUEST_DATE;
                            entity.L1_USER = obj.L1_USER;
                            entity.L1_DATE = obj.L1_DATE;
                            entity.L1_NOTE = obj.L1_NOTE;
                            entity.L2_USER = obj.L2_USER;
                            entity.L2_DATE = obj.L2_DATE;
                            entity.L2_NOTE = obj.L2_NOTE;
                            entity.END_USER = obj.END_USER;
                            entity.END_DATE = obj.END_DATE;
                            entity.END_NOTE = obj.END_NOTE;
                            entity.WORK_REMARKS = obj.WORK_REMARKS;
                            entity.WORK_FILE = obj.WORK_FILE;
                            entity.WORK_START_DATE = obj.L1_DATE == null ? obj.WORK_START_DATE : obj.L1_DATE; //Level 1 date will be start date
                            entity.WORK_END_DATE = obj.WORK_END_DATE;
                            //Start Audit
                            entity.IS_ACTIVE = obj.IS_ACTIVE;
                            entity.UPDATE_USER = userId;
                            entity.UPDATE_DATE = dateTime;
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
                string error = ex.Message.Contains("See the inner exception for details")
                                ? ex.InnerException?.Message ?? ex.Message
                                : ex.Message;
                error = error.Replace("'", "");
                eQResult.messages = NotifyService.Error(error);
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }

        public List<WORK_TASK> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM WORK_TASK BI
                    ORDER BY BI.IS_ACTIVE DESC, BI.REQUEST_DATE";
            return dbCtx.Database.SqlQuery<WORK_TASK>(sql).ToList();
        }
        public List<WORK_TASK> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM WORK_TASK BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.REQUEST_DATE";
            return dbCtx.Database.SqlQuery<WORK_TASK>(sql).ToList();
        }
        public WORK_TASK GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM WORK_TASK BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<WORK_TASK>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "WORK_TASK";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.WORK_TASK.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.WORK_TASK.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.TASK_TITLE!);
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
                string error = ex.Message.Contains("See the inner exception for details")
                              ? ex.InnerException?.Message ?? ex.Message
                              : ex.Message;
                error = error.Replace("'", "");
                eQResult.messages = error;
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
    }
}