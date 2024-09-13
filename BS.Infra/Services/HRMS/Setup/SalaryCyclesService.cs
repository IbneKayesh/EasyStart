namespace BS.Infra.Services.HRMS.Setup
{
    public class SalaryCyclesService
    {
        private readonly AppDbContext dbCtx;
        public SalaryCyclesService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(SALARY_CYCLES obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SALARY_CYCLES";
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

                    dbCtx.SALARY_CYCLES.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.SALARY_CYCLES.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.CYCLE_NAME = obj.CYCLE_NAME;
                            entity.START_DAY = obj.START_DAY;
                            entity.END_DAY = obj.END_DAY;
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

        public List<SALARY_CYCLES> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SALARY_CYCLES BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.CYCLE_NAME";
            return dbCtx.Database.SqlQuery<SALARY_CYCLES>(sql).ToList();
        }
        public List<SALARY_CYCLES> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SALARY_CYCLES BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.CYCLE_NAME";
            return dbCtx.Database.SqlQuery<SALARY_CYCLES>(sql).ToList();
        }
        public SALARY_CYCLES GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SALARY_CYCLES BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<SALARY_CYCLES>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SALARY_CYCLES";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //old entity
                var entity = dbCtx.SALARY_CYCLES.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.SALARY_CYCLES.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.CYCLE_NAME!);
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
