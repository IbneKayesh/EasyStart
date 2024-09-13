namespace BS.Infra.Services.HRMS.Attendance
{
    public class AttendanceLogService
    {
        private readonly AppDbContext dbCtx;
        public AttendanceLogService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(ATTENDANCE_LOG obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ATTENDANCE_LOG";
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

                    dbCtx.ATTENDANCE_LOG.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.ATTENDANCE_LOG.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            //entity.COUNTRY_ID = obj.COUNTRY_ID;
                            //entity.BRAND_NAME = obj.BRAND_NAME;
                            //entity.BRAND_DESC = obj.BRAND_DESC;
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

        //        public List<ATTENDANCE_LOG_VM> GetAll()
        //        {
        //            FormattableString sql = $@"SELECT BI.*,CI.COUNTRY_NAME
        //                    FROM ATTENDANCE_LOG BI
        //JOIN COUNTRY_INFO CI on BI.COUNTRY_ID = CI.ID
        //                    ORDER BY CI.COUNTRY_NAME, BI.BRAND_NAME";
        //            return dbCtx.Database.SqlQuery<ATTENDANCE_LOG_VM>(sql).ToList();
        //        }
        public List<ATTENDANCE_LOG> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ATTENDANCE_LOG BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.BRAND_NAME";
            return dbCtx.Database.SqlQuery<ATTENDANCE_LOG>(sql).ToList();
        }
        public ATTENDANCE_LOG GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ATTENDANCE_LOG BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<ATTENDANCE_LOG>(sql).ToList().FirstOrDefault();
        }
        
        public List<ATTENDANCE_LOG> GetByEmpId(string empId)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ATTENDANCE_LOG BI
                    WHERE BI.EMP_ID = {empId} order by bi.ATTEN_DATE DESC";
            return dbCtx.Database.SqlQuery<ATTENDANCE_LOG>(sql).ToList();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ATTENDANCE_LOG";
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
                var entity = dbCtx.ATTENDANCE_LOG.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.ATTENDANCE_LOG.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.EMP_ID!);
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


        public EQResult ProcessByLoginLog(string from_date, string to_date, string empId, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ATTENDANCE_LOG";
            if (string.IsNullOrWhiteSpace(from_date) || string.IsNullOrWhiteSpace(to_date))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                DateTime fd = DateTime.Parse(from_date);
                DateTime td = DateTime.Parse(to_date);
                int total_days = (td - fd).Days;
                int r = 0;

                var oldlog = dbCtx.ATTENDANCE_LOG.Where(x => x.EMP_ID == empId && x.ATTEN_DATE.Date >= fd.Date && x.ATTEN_DATE <=td.Date).ToList();
                dbCtx.ATTENDANCE_LOG.RemoveRange(oldlog);
                dbCtx.SaveChanges();

                for (int i = 1; i <= total_days; i++)
                {
                    if (fd.Date == DateTime.Now.AddDays(2).Date)
                    {
                        eQResult.messages = "Could not procced today";
                        return eQResult;
                    }
                    ATTENDANCE_LOG obj = new ATTENDANCE_LOG();
                    DateTime dateTime = new DateTime();
                    var logData = dbCtx.USER_LOGIN_INFO.Where(x => x.USER_ID == empId && x.IN_TIME.Date == fd.Date).ToList();
                    if (logData.Count > 0)
                    {
                        //has attend
                        dateTime = logData.Min(x => x.IN_TIME);
                        obj.ATTEN_NOTE = "Present";
                    }
                    else
                    {
                        obj.IN_TIME = (dateTime == DateTime.MinValue) ? (DateTime?)null : dateTime;
                        obj.ATTEN_NOTE = "Absent";
                    }


                    obj.EMP_ID = empId;
                    obj.ATTEN_DATE = fd;
                    obj.ATTEN_TYPE_ID = "PRESENT_ID";
                    obj.WORK_SHIFT_ID = "WS_ID";

                    obj.TOTAL_TIME = 0;
                    obj.OVER_TIME = 0;
                    obj.NET_TIME = 0;



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

                    dbCtx.ATTENDANCE_LOG.Add(obj);
                    r += dbCtx.SaveChanges();

                    fd = fd.AddDays(1);
                }
                eQResult.rows = r;
                eQResult.success = true;
                eQResult.messages = NotifyService.SaveSuccess();
                return eQResult;
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
