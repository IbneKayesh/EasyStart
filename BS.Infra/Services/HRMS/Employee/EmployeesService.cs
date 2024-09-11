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
                eQResult.messages = NotifyService.Error(ex.Message == string.Empty ? ex.InnerException.Message : ex.Message);
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
        

    }
}
