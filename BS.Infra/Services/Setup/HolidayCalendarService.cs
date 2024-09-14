using Microsoft.AspNetCore.Mvc;

namespace BS.Infra.Services.Setup
{
    public class HolidayCalendarService
    {
        private readonly AppDbContext dbCtx;
        public HolidayCalendarService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(HOLIDAY_CALENDAR obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "HOLIDAY_CALENDAR";
            try
            {

                if (obj.ID == Guid.Empty.ToString())
                {
                    var entity_date = dbCtx.HOLIDAY_CALENDAR.Where(x => x.CALENDAR_DATE.Date == obj.CALENDAR_DATE.Date).ToList();
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

                    dbCtx.HOLIDAY_CALENDAR.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.HOLIDAY_CALENDAR.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.FINANCIAL_YEAR_ID = obj.FINANCIAL_YEAR_ID;
                            entity.HOLIDAY_TYPE_ID = obj.HOLIDAY_TYPE_ID;
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

        public List<HOLIDAY_CALENDAR_VM> GetAll(string financialYearID, string holidayTypeID)
        {
            string criteria = string.Empty;
            List<object> param = new List<object>();

            if (!string.IsNullOrWhiteSpace(financialYearID))
            {
                criteria = "Where FY.ID = @FINANCIAL_YEAR_ID";
                param.Add(new SqlParameter(parameterName: "FINANCIAL_YEAR_ID", financialYearID));
            }
            if (!string.IsNullOrWhiteSpace(holidayTypeID))
            {
                criteria = "Where LT.ID = @HOLIDAY_TYPE_ID";
                param.Add(new SqlParameter(parameterName: "HOLIDAY_TYPE_ID", holidayTypeID));

                criteria += " and FY.ID = @FINANCIAL_YEAR_ID";
                param.Add(new SqlParameter(parameterName: "FINANCIAL_YEAR_ID", DateTime.Now.Year));
            }

            string sql = $@"select LC.*,FY.YEAR_NAME,LT.HOLIDAY_TYPE_NAME
From HOLIDAY_CALENDAR LC
JOIN FINANCIAL_YEAR FY on LC.FINANCIAL_YEAR_ID = FY.ID
JOIN HOLIDAY_TYPE LT on LC.HOLIDAY_TYPE_ID = LT.ID {criteria}
ORDER BY LC.CALENDAR_DATE";
            return dbCtx.Database.SqlQueryRaw<HOLIDAY_CALENDAR_VM>(sql, param.ToArray()).ToList();
        }
        public List<HOLIDAY_CALENDAR> GetAllActive()
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM HOLIDAY_CALENDAR LT
                    WHERE LT.IS_ACTIVE = 1
                    ORDER BY LT.CALENDAR_DATE";
            return dbCtx.Database.SqlQuery<HOLIDAY_CALENDAR>(sql).ToList();
        }
        public HOLIDAY_CALENDAR GetById(string id)
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM HOLIDAY_CALENDAR LT
                    WHERE LT.ID = {id}";
            return dbCtx.Database.SqlQuery<HOLIDAY_CALENDAR>(sql).ToList().FirstOrDefault()!;
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "HOLIDAY_CALENDAR";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.HOLIDAY_CALENDAR.Where(x => x.ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.HOLIDAY_CALENDAR.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.HOLIDAY_CALENDAR.Remove(entity);
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


        //yearly leave calendar / leave allocation
        public EQResult InsertYearlyLeaveCalendar(YEARLY_LEAVE_CALENDAR obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "YEARLY_LEAVE_CALENDAR";
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

                    dbCtx.YEARLY_LEAVE_CALENDAR.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.YEARLY_LEAVE_CALENDAR.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.FINANCIAL_YEAR_ID = obj.FINANCIAL_YEAR_ID;
                            entity.HOLIDAY_TYPE_ID = obj.HOLIDAY_TYPE_ID;
                            entity.NO_OF_LEAVE = obj.NO_OF_LEAVE;
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

        public List<LEAVE_CALENDAR_VM> GetAllYearlyLeaveCalendar(string financialYearID, string holidayTypeID)
        {
            string criteria = string.Empty;
            List<object> param = new List<object>();

            if (!string.IsNullOrWhiteSpace(financialYearID))
            {
                criteria = "Where FY.ID = @FINANCIAL_YEAR_ID";
                param.Add(new SqlParameter(parameterName: "FINANCIAL_YEAR_ID", financialYearID));
            }
            if (string.IsNullOrWhiteSpace(financialYearID) && !string.IsNullOrWhiteSpace(holidayTypeID))
            {
                criteria = "Where LT.ID = @HOLIDAY_TYPE_ID";
                param.Add(new SqlParameter(parameterName: "HOLIDAY_TYPE_ID", holidayTypeID));

                criteria += " and FY.ID = @FINANCIAL_YEAR_ID";
                param.Add(new SqlParameter(parameterName: "FINANCIAL_YEAR_ID", DateTime.Now.Year));
            }

            string sql = $@"select LC.*,FY.YEAR_NAME,LT.HOLIDAY_TYPE_NAME
From YEARLY_LEAVE_CALENDAR LC
JOIN FINANCIAL_YEAR FY on LC.FINANCIAL_YEAR_ID = FY.ID
JOIN HOLIDAY_TYPE LT on LC.HOLIDAY_TYPE_ID = LT.ID {criteria}";
            var FirstResult = dbCtx.Database.SqlQueryRaw<LEAVE_CALENDAR_VM>(sql, param.ToArray()).ToList();

            //avoid complexity of union all

            sql = $@"WITH HC AS(
SELECT '' ID, HC.FINANCIAL_YEAR_ID,HC.HOLIDAY_TYPE_ID, COUNT(CALENDAR_DATE)NO_OF_LEAVE
FROM HOLIDAY_CALENDAR HC
GROUP BY HC.FINANCIAL_YEAR_ID,HC.HOLIDAY_TYPE_ID
)
SELECT HC.*,CAST(1 AS BIT) IS_ACTIVE, '' CREATE_USER, GETDATE() CREATE_DATE,'' UPDATE_USER, GETDATE() UPDATE_DATE,0 REVISE_NO, CAST('0x' as timestamp) ROWVERSION,
FY.YEAR_NAME,LT.HOLIDAY_TYPE_NAME
FROM HC
JOIN FINANCIAL_YEAR FY ON HC.FINANCIAL_YEAR_ID = FY.ID
JOIN HOLIDAY_TYPE LT ON HC.HOLIDAY_TYPE_ID = LT.ID {criteria}";
            var SecondResult = dbCtx.Database.SqlQueryRaw<LEAVE_CALENDAR_VM>(sql, param.ToArray()).ToList();

            if (SecondResult.Count > 0)
            {
                FirstResult.AddRange(SecondResult);
            }
            return FirstResult;
        }

        public YEARLY_LEAVE_CALENDAR GetYearlyLeaveCalendarById(string id)
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM YEARLY_LEAVE_CALENDAR LT
                    WHERE LT.ID = {id}";
            return dbCtx.Database.SqlQuery<YEARLY_LEAVE_CALENDAR>(sql).ToList().FirstOrDefault()!;
        }

        public EQResult DeleteYearlyLeaveCalendar(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "YEARLY_LEAVE_CALENDAR";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.HOLIDAY_CALENDAR.Where(x => x.ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.YEARLY_LEAVE_CALENDAR.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.YEARLY_LEAVE_CALENDAR.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.NO_OF_LEAVE.ToString());
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


        //process employee leave balance

        public EQResult ProcessEmployeeYearlyLeaveCalendar(string financialYearID, string userId)
        {

            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_LEAVE_BALANCE";

            var empList = dbCtx.EMPLOYEES.Where(x => x.IS_ACTIVE == true).ToList();

            List<LEAVE_CALENDAR_VM> leaveList = GetAllYearlyLeaveCalendar(financialYearID, "");

            List<EMP_LEAVE_BALANCE> delete_elb = new List<EMP_LEAVE_BALANCE>();
            List<EMP_LEAVE_BALANCE> insert_elb = new List<EMP_LEAVE_BALANCE>();

            foreach (var emp in empList)
            {
                //delete them if its not in use
                // && x.USED_QTY == 0
                delete_elb.AddRange(dbCtx.EMP_LEAVE_BALANCE.Where(x => x.FINANCIAL_YEAR_ID == financialYearID && x.EMP_ID == emp.ID));
                foreach (var leave in leaveList)
                {
                    EMP_LEAVE_BALANCE _elb = new EMP_LEAVE_BALANCE();
                    _elb.ID = Guid.NewGuid().ToString();
                    _elb.EMP_ID = emp.ID;
                    _elb.FINANCIAL_YEAR_ID = financialYearID;
                    _elb.HOLIDAY_TYPE_ID = leave.HOLIDAY_TYPE_ID;
                    _elb.NO_OF_LEAVE = leave.NO_OF_LEAVE;
                    _elb.USED_QTY = 0;
                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    _elb.CREATE_USER = userId;
                    _elb.CREATE_DATE = DateTime.Now;
                    _elb.UPDATE_USER = userId;
                    _elb.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    insert_elb.Add(_elb);
                }
            }           
            dbCtx.EMP_LEAVE_BALANCE.RemoveRange(delete_elb);
            dbCtx.EMP_LEAVE_BALANCE.AddRangeAsync(insert_elb);
            eQResult.rows = dbCtx.SaveChanges();
            eQResult.success = true;
            eQResult.messages = NotifyService.SaveSuccess();
            return eQResult;
        }
    }

}
