namespace BS.Infra.Services.HelpDesk.Setup
{
    public class RoutineNamesService
    {
        private readonly AppDbContext dbCtx;
        public RoutineNamesService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(TASK_STATUS obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "TASK_STATUS";
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

                    dbCtx.TASK_STATUS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.TASK_STATUS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            //entity.TASK_STATUS_NAME = obj.TASK_STATUS_NAME;
                            //entity.SHORT_NAME = obj.SHORT_NAME;
                            //entity.ADDRESS_INFO = obj.ADDRESS_INFO;
                            //entity.CONTACT_NAME = obj.CONTACT_NAME;
                            //entity.CONTACT_NO = obj.CONTACT_NO;
                            //entity.EMAIL_ADDRESS = obj.EMAIL_ADDRESS;
                            //entity.MAX_EMPLOYEE = obj.MAX_EMPLOYEE;
                            //entity.MAX_SALARY = obj.MAX_SALARY;
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

        public List<TASK_STATUS> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM TASK_STATUS BI
                    ORDER BY BI.STATUS_NAME";
            return dbCtx.Database.SqlQuery<TASK_STATUS>(sql).ToList();
        }
        public List<ROUTINE_NAMES> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ROUTINE_NAMES BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.ROUTINE_NAME";
            return dbCtx.Database.SqlQuery<ROUTINE_NAMES>(sql).ToList();
        }
        public TASK_STATUS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM TASK_STATUS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<TASK_STATUS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "TASK_STATUS";
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
                var entity = dbCtx.TASK_STATUS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.TASK_STATUS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.STATUS_NAME!);
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
