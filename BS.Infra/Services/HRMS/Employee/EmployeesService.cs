using BS.Helper;

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
            if (obj.BIRTH_DATE.Date == DateTime.Now.Date)
            {
                eQResult.success = false;
                eQResult.messages = "Enter correct birthdate";
                return eQResult;
            }
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
                    //APP_NOTIFICATIONS notif = new APP_NOTIFICATIONS();
                    //notif.TITLE_TEXT = "Employee";
                    //notif.BODY_TEXT = $"Employee Inserted - {obj.EMP_NO} - {obj.EMP_NAME}";
                    //notif.NAV_URL = "";
                    //notif.TO_USER = "";
                    //notif.TO_USER_GROUP = "";
                    //notif.FROM_USER = userId;
                    //notif.PRIORITY_LEVEL = "High";
                    //notif.IS_READ = false;
                    //InsertAppNotifications(dbCtx, notif, userId);
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
                            //TODO : Update propertyentity.ID = obj.ID;
                            entity.EMP_NO = obj.EMP_NO;
                            entity.EMP_NAME = obj.EMP_NAME;
                            entity.EMP_PHOTO = obj.EMP_PHOTO;
                            entity.CARD_NO1 = obj.CARD_NO1;
                            entity.CARD_NO2 = obj.CARD_NO2;
                            entity.CONTACT_NO = obj.CONTACT_NO;
                            entity.EMAIL_ADDRESS = obj.EMAIL_ADDRESS;
                            entity.BIRTH_DATE = obj.BIRTH_DATE;
                            entity.GENDER_ID = obj.GENDER_ID;
                            entity.MARITAL_STATUS = obj.MARITAL_STATUS;
                            entity.SPOUSE_NAME = obj.SPOUSE_NAME;
                            entity.BLOOD_GROUP = obj.BLOOD_GROUP;
                            entity.NATIONAL_IDNO = obj.NATIONAL_IDNO;
                            entity.PASSPORT_NO = obj.PASSPORT_NO;
                            entity.TIN_NO = obj.TIN_NO;
                            entity.NATIONALITY = obj.NATIONALITY;
                            entity.FATHER_NAME = obj.FATHER_NAME;
                            entity.MOTHER_NAME = obj.MOTHER_NAME;
                            entity.PARENTS_CONACT = obj.PARENTS_CONACT;
                            entity.REFERENCE_PERSON = obj.REFERENCE_PERSON;
                            entity.JOIN_DATE = obj.JOIN_DATE;
                            entity.CONFIRM_DATE = obj.CONFIRM_DATE;
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
                            //APP_NOTIFICATIONS notif = new APP_NOTIFICATIONS();
                            //notif.TITLE_TEXT = "Employee";
                            //notif.BODY_TEXT = $"Employee Edited - {obj.EMP_NO} - {obj.EMP_NAME}";
                            //notif.NAV_URL = "";
                            //notif.TO_USER = "";
                            //notif.TO_USER_GROUP = "";
                            //notif.FROM_USER = userId;
                            //notif.PRIORITY_LEVEL = "High";
                            //notif.IS_READ = false;
                            //InsertAppNotifications(dbCtx, notif, userId);
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
            string sql = $@"SELECT BI.* FROM EMPLOYEES BI WHERE BI.ID = '{id}'";
            var entity = dbCtx.Database.SqlQueryRaw<EMPLOYEES>(sql).ToList().FirstOrDefault();
            if (entity == null)
            {
                return new EMPLOYEES();
            }
            sql = $"select * from EMP_ADDRESS where EMP_ID = '{entity.ID}'";
            entity.EMP_ADDRESS = dbCtx.Database.SqlQueryRaw<EMP_ADDRESS>(sql).ToList();

            sql = $"select * from EMP_EXPERIENCE where EMP_ID = '{entity.ID}'";
            entity.EMP_EXPERIENCE = dbCtx.Database.SqlQueryRaw<EMP_EXPERIENCE>(sql).ToList();

            sql = $"select * from EMP_EDU where EMP_ID = '{entity.ID}'";
            entity.EMP_EDU = dbCtx.Database.SqlQueryRaw<EMP_EDU>(sql).ToList();

            sql = $@"SELECT ED.*,DG.DESIGNATION_NAME,SS.SUB_SECTION_NAME
                     FROM EMP_DESIG ED
                     JOIN DESIGNATION DG ON ED.DESIG_ID = DG.ID
                     JOIN SUB_SECTIONS SS ON ED.SUB_SECTION_ID = SS.ID
                     WHERE ED.EMP_ID = '{entity.ID}'";
            entity.EMP_DESIG_VM = dbCtx.Database.SqlQueryRaw<EMP_DESIG_VM>(sql).ToList();

            sql = $@"SELECT ESC.*,SC.CYCLE_NAME FROM EMP_SALARY_CYCLES ESC JOIN SALARY_CYCLES SC ON ESC.SALARY_CYCLES_ID = SC.ID WHERE ESC.EMP_ID = '{entity.ID}'";
            entity.EMP_SALARY_CYCLES_VM = dbCtx.Database.SqlQueryRaw<EMP_SALARY_CYCLES_VM>(sql).ToList();

            sql = $@"SELECT LC.*,FY.YEAR_NAME,LT.HOLIDAY_TYPE_NAME
FROM EMP_LEAVE_BALANCE LC
JOIN FINANCIAL_YEAR FY ON LC.FINANCIAL_YEAR_ID = FY.ID
JOIN HOLIDAY_TYPE LT ON LC.HOLIDAY_TYPE_ID = LT.ID 
WHERE LC.EMP_ID = '{entity.ID}' ORDER BY LC.FINANCIAL_YEAR_ID DESC, LT.IS_APPLICATION_REQUIRED DESC";
            entity.EMP_LEAVE_BALANCE_VM = dbCtx.Database.SqlQueryRaw<EMP_LEAVE_BALANCE_VM>(sql).ToList();

            return entity;
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
            return dbCtx.EMPLOYEES.Where(x => x.IS_ACTIVE == true &&
                                    (x.EMP_NO.Contains(empName) ||
                                    x.EMP_NAME.Contains(empName))
                                    ).ToList();
        }
        //address
        public EQResult InsertAddress(EMP_ADDRESS obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_ADDRESS";
            if (obj.EMP_ID == Guid.Empty.ToString())
            {
                eQResult.success = false;
                eQResult.messages = "Save employee information first";
                return eQResult;
            }
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

                    dbCtx.EMP_ADDRESS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();

                    //Notification
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.EMP_ADDRESS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.IS_PRESENT = obj.IS_PRESENT;
                            entity.ADDRESS_DETAIL = obj.ADDRESS_DETAIL;
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
                eQResult.messages = ex.Message == string.Empty ? ex.InnerException.Message : ex.Message;
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
        public EMP_ADDRESS GetAddressByID(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM EMP_ADDRESS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<EMP_ADDRESS>(sql).ToList().FirstOrDefault();
        }
        //experience
        public EMP_EXPERIENCE GetExperienceByID(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM EMP_EXPERIENCE BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<EMP_EXPERIENCE>(sql).ToList().FirstOrDefault();
        }
        public EQResult InsertExperience(EMP_EXPERIENCE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_EXPERIENCE";
            if (obj.EMP_ID == Guid.Empty.ToString())
            {
                eQResult.success = false;
                eQResult.messages = "Save employee information first";
                return eQResult;
            }
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

                    dbCtx.EMP_EXPERIENCE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();

                    //Notification
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.EMP_EXPERIENCE.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.COMPANY_NAME = obj.COMPANY_NAME;
                            entity.WORKING_AREA = obj.WORKING_AREA;
                            entity.EXP_YEAR = obj.EXP_YEAR;
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
        //education
        public EMP_EDU GetEduByID(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM EMP_EDU BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<EMP_EDU>(sql).ToList().FirstOrDefault();
        }
        public EQResult InsertEdu(EMP_EDU obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_EDU";
            if (obj.EMP_ID == Guid.Empty.ToString())
            {
                eQResult.success = false;
                eQResult.messages = "Save employee information first";
                return eQResult;
            }
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

                    dbCtx.EMP_EDU.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();

                    //Notification
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.EMP_EDU.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.IS_CERTIFICATE = obj.IS_CERTIFICATE;
                            entity.EDU_TITLE = obj.EDU_TITLE;
                            entity.EDU_YEAR = obj.EDU_YEAR;
                            entity.EDU_GRADE = obj.EDU_GRADE;
                            entity.INSTITUE_NAME = obj.INSTITUE_NAME;
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
        //designation
        public EMP_DESIG GetDesignationByID(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM EMP_DESIG BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<EMP_DESIG>(sql).ToList().FirstOrDefault();
        }
        public EQResult InsertDesignation(EMP_DESIG obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_DESIG";
            if (obj.EMP_ID == Guid.Empty.ToString())
            {
                eQResult.success = false;
                eQResult.messages = NotifyService.Warning("Save employee information first");
                return eQResult;
            }
            if (obj.FROM_DATE > obj.TO_DATE)
            {
                eQResult.success = false;
                eQResult.messages = NotifyService.Warning("To date must be ahead from start date");
                return eQResult;
            }
            //for edit
            if (obj.ID != Guid.Empty.ToString())
            {
                var old_entity = dbCtx.EMP_DESIG.Where(x => x.EMP_ID == obj.EMP_ID && x.ID != obj.ID).ToList();
                if (old_entity.Count() > 0)
                {
                    DateTime MaxEndDate = DateTimeHelper.FindMinDate(old_entity.Select(x => x.TO_DATE).ToList());
                    if (MaxEndDate > obj.FROM_DATE)
                    {
                        eQResult.success = false;
                        eQResult.messages = NotifyService.Warning("Start date must be ahead from last designation end date");
                        return eQResult;
                    }
                }
            }
            else
            {
                //for new entry
                var old_entity = dbCtx.EMP_DESIG.Where(x => x.EMP_ID == obj.EMP_ID).ToList();
                if (old_entity.Count() > 0)
                {
                    DateTime MaxEndDate = DateTimeHelper.FindMinDate(old_entity.Select(x => x.TO_DATE).ToList());
                    if (MaxEndDate > obj.FROM_DATE)
                    {
                        eQResult.success = false;
                        eQResult.messages = NotifyService.Warning("Start date must be ahead from last designation end date");
                        return eQResult;
                    }
                }
            }

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

                    dbCtx.EMP_DESIG.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();

                    //Notification
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.EMP_DESIG.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.DESIG_ID = obj.DESIG_ID;
                            entity.SUB_SECTION_ID = obj.SUB_SECTION_ID;
                            entity.FROM_DATE = obj.FROM_DATE;
                            entity.TO_DATE = obj.TO_DATE;
                            entity.DESIG_NOTE = obj.DESIG_NOTE;
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

        //salary cycles
        public EMP_SALARY_CYCLES GetSalaryCyclesByID(string id)
        {
            FormattableString sql = $@"SELECT BI.* FROM EMP_SALARY_CYCLES BI WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<EMP_SALARY_CYCLES>(sql).ToList().FirstOrDefault();
        }
        public EQResult InsertSalaryCycles(EMP_SALARY_CYCLES obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_SALARY_CYCLES";
            if (obj.EMP_ID == Guid.Empty.ToString())
            {
                eQResult.success = false;
                eQResult.messages = NotifyService.Warning("Save employee information first");
                return eQResult;
            }
            if (obj.ID == Guid.Empty.ToString())
            {
                bool entity = dbCtx.EMP_SALARY_CYCLES.Where(x => x.EMP_ID == obj.EMP_ID).Any();
                if (entity)
                {
                    eQResult.success = false;
                    eQResult.messages = NotifyService.Warning("Unable to add multiple salary cycle");
                    return eQResult;
                }
            }
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

                    dbCtx.EMP_SALARY_CYCLES.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();

                    //Notification
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.EMP_SALARY_CYCLES.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            //entity.EMP_ID = obj.EMP_ID;
                            entity.SALARY_CYCLES_ID = obj.SALARY_CYCLES_ID;
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
    }
}
