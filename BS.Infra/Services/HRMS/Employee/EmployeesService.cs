namespace BS.Infra.Services.HRMS.Employee
{
    public class EmployeesService : BaseService
    {
        private readonly AppDbContext dbCtx;
        public EmployeesService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(EMPLOYEES obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMPLOYEES";
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

                    dbCtx.EMPLOYEES.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();

                    //Notification
                    APP_NOTIFICATIONS notif = new APP_NOTIFICATIONS();
                    notif.TITLE_TEXT = "Employee";
                    notif.BODY_TEXT = $"Employee Inserted - {obj.EMP_NO} - {obj.EMP_NAME}";
                    notif.NAV_URL = "";
                    notif.TO_USER = "";
                    notif.TO_USER_GROUP = "";
                    notif.FROM_USER = userId;
                    notif.PRIORITY_LEVEL = "High";
                    notif.IS_READ = false;
                    InsertAppNotifications(dbCtx, notif, userId);
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.EMPLOYEES.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.EMP_NO = obj.EMP_NO;
                            entity.EMP_NAME = obj.EMP_NAME;
                            entity.DESIG_ID = obj.DESIG_ID;
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


                            //Notification
                            APP_NOTIFICATIONS notif = new APP_NOTIFICATIONS();
                            notif.TITLE_TEXT = "Employee";
                            notif.BODY_TEXT = $"Employee Edited - {obj.EMP_NO} - {obj.EMP_NAME}";
                            notif.NAV_URL = "";
                            notif.TO_USER = "";
                            notif.TO_USER_GROUP = "";
                            notif.FROM_USER = userId;
                            notif.PRIORITY_LEVEL = "High";
                            notif.IS_READ = false;
                            InsertAppNotifications(dbCtx, notif, userId);
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

        public List<EMPLOYEES> GetAll()
        {
            FormattableString sql = $@"select d.*
                                    from EMPLOYEES d order by d.EMP_NAME";
            return dbCtx.Database.SqlQuery<EMPLOYEES>(sql).ToList();
        }
        public List<EMPLOYEES> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM EMPLOYEES BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.EMP_NAME";
            return dbCtx.Database.SqlQuery<EMPLOYEES>(sql).ToList();
        }
        public EMPLOYEES GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM EMPLOYEES BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<EMPLOYEES>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMPLOYEES";
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
                var entity = dbCtx.EMPLOYEES.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.EMPLOYEES.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.EMP_NAME!);


                    //Notification
                    APP_NOTIFICATIONS notif = new APP_NOTIFICATIONS();
                    notif.TITLE_TEXT = "Employee";
                    notif.BODY_TEXT = $"Employee Inserted - {entity.EMP_NO} - {entity.EMP_NAME}";
                    notif.NAV_URL = "";
                    notif.TO_USER = "";
                    notif.TO_USER_GROUP = "";
                    notif.FROM_USER = userId;
                    notif.PRIORITY_LEVEL = "High";
                    notif.IS_READ = false;
                    InsertAppNotifications(dbCtx, notif, userId);
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

        public List<EMPLOYEES> GetByName(string empName)
        {
            return dbCtx.EMPLOYEES.Where(x => x.IS_ACTIVE ==true &&  
                                    (x.EMP_NO.Contains(empName) || 
                                    x.EMP_NAME.Contains(empName))
                                    ).ToList();
        }
    }
}
