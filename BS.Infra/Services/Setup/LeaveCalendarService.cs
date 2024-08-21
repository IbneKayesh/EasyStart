using BS.DMO.StaticValues;
using Microsoft.Data.SqlClient;

namespace BS.Infra.Services.Setup
{
    public class LeaveCalendarService
    {
        private readonly AppDbContext dbCtx;
        public LeaveCalendarService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(LEAVE_CALENDAR obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "LEAVE_CALENDAR";
            try
            {

                if (obj.ID == Guid.Empty.ToString())
                {
                    var entity_date = dbCtx.LEAVE_CALENDAR.Where(x => x.CALENDAR_DATE.Date == obj.CALENDAR_DATE.Date).ToList();
                    if (entity_date.Count > 0)
                    {
                        eQResult.messages = NotifyService.Error($"{obj.CALENDAR_DATE.ToString(AppDateFormat.DATE_DISPLAY_FORMAT)} is already added");
                        return eQResult;
                    }

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

                    dbCtx.LEAVE_CALENDAR.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.LEAVE_CALENDAR.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.FINANCIAL_YEAR_ID = obj.FINANCIAL_YEAR_ID;
                            entity.LEAVE_TYPE_ID = obj.LEAVE_TYPE_ID;
                            //entity.CALENDAR_DATE = obj.CALENDAR_DATE; :: excluding date changes
                            entity.IS_WORKING_DAY = obj.IS_WORKING_DAY;
                            entity.NOTE_INFO = obj.NOTE_INFO;
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

        public List<LEAVE_CALENDAR_VM> GetAll(string financialYearID, string leaveTypeID)
        {
            string criteria = string.Empty;
            List<object> param = new List<object>();

            if (!string.IsNullOrWhiteSpace(financialYearID))
            {
                criteria = "Where FY.ID = @FINANCIAL_YEAR_ID";
                param.Add(new SqlParameter(parameterName: "FINANCIAL_YEAR_ID", financialYearID));
            }
            if (!string.IsNullOrWhiteSpace(leaveTypeID))
            {
                criteria = "Where LT.ID = @LEAVE_TYPE_ID";
                param.Add(new SqlParameter(parameterName: "LEAVE_TYPE_ID", leaveTypeID));

                criteria += " and FY.ID = @FINANCIAL_YEAR_ID";
                param.Add(new SqlParameter(parameterName: "FINANCIAL_YEAR_ID", DateTime.Now.Year));
            }

            string sql = $@"select LC.*,FY.YEAR_NAME,LT.LEAVE_TYPE_NAME
From LEAVE_CALENDAR LC
JOIN FINANCIAL_YEAR FY on LC.FINANCIAL_YEAR_ID = FY.ID
JOIN LEAVE_TYPE LT on LC.LEAVE_TYPE_ID = LT.ID {criteria}
ORDER BY LC.CALENDAR_DATE";
            return dbCtx.Database.SqlQueryRaw<LEAVE_CALENDAR_VM>(sql, param.ToArray()).ToList();
        }
        public List<LEAVE_CALENDAR> GetAllActive()
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM LEAVE_CALENDAR LT
                    WHERE LT.IS_ACTIVE = 1
                    ORDER BY LT.CALENDAR_DATE";
            return dbCtx.Database.SqlQuery<LEAVE_CALENDAR>(sql).ToList();
        }
        public LEAVE_CALENDAR GetById(string id)
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM LEAVE_CALENDAR LT
                    WHERE LT.ID = {id}";
            return dbCtx.Database.SqlQuery<LEAVE_CALENDAR>(sql).ToList().FirstOrDefault()!;
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "LEAVE_CALENDAR";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.LEAVE_CALENDAR.Where(x => x.ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.LEAVE_CALENDAR.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.LEAVE_CALENDAR.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.CALENDAR_DATE.ToString(AppDateFormat.DATE_DISPLAY_FORMAT)!);
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
