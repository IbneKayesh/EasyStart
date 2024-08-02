namespace BS.Infra.Services.Setup
{
    public class FinancialYearService
    {
        private readonly AppDbContext dbCtx;
        public FinancialYearService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(FINANCIAL_YEAR obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "FINANCIAL_YEAR";
            try
            {
                //old entity
                var entity = dbCtx.FINANCIAL_YEAR.Find(obj.ID);
                if (entity != null)
                {
                    //special checking for empty byte, cause user Enter ID without edit mode
                    byte[] emptyByte = obj.RowVersion == null ? Enumerable.Repeat((byte)0x20, 100).ToArray() : obj.RowVersion;
                   
                    if (entity.RowVersion.SequenceEqual(emptyByte))
                    {
                        //TODO : Update property
                        entity.YEAR_NAME = obj.YEAR_NAME;
                        entity.START_DATE = obj.START_DATE;
                        entity.END_DATE = obj.END_DATE;
                        entity.IS_LOCKED = obj.IS_LOCKED;
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
                    //new entity

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.FINANCIAL_YEAR.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
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

        public List<FINANCIAL_YEAR> GetAll()
        {
            FormattableString sql = $@"SELECT FI.*
                    FROM FINANCIAL_YEAR FI
                    ORDER BY FI.ID";
            return dbCtx.Database.SqlQuery<FINANCIAL_YEAR>(sql).ToList();
        }
        public List<FINANCIAL_YEAR> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM FINANCIAL_YEAR BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.YEAR_NAME";
            return dbCtx.Database.SqlQuery<FINANCIAL_YEAR>(sql).ToList();
        }
        public FINANCIAL_YEAR GetById(string id)
        {
            FormattableString sql = $@"SELECT FY.*
                    FROM FINANCIAL_YEAR FY
                    WHERE FY.ID = {id}";
            return dbCtx.Database.SqlQuery<FINANCIAL_YEAR>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "FINANCIAL_YEAR";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                int anyChild = dbCtx.LEAVE_CALENDAR.Where(x => x.FINANCIAL_YEAR_ID == id).Count();
                if (anyChild > 0)
                {
                    eQResult.messages = NotifyService.DeleteHasChildString("Leave calendar", anyChild, "Financial Year");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.FINANCIAL_YEAR.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.FINANCIAL_YEAR.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.YEAR_NAME!);
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
