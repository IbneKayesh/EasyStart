namespace BS.Infra.Services.Application
{
    public class AppNotificationsService
    {
        private readonly AppDbContext dbCtx;
        public AppNotificationsService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(APP_NOTIFICATIONS obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "APP_NOTIFICATIONS";
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

                    dbCtx.APP_NOTIFICATIONS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.APP_NOTIFICATIONS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            //entity.APP_NOTIFICATIONS_TYPE_ID = obj.APP_NOTIFICATIONS_TYPE_ID;
                            //entity.BUSINESS_ID = obj.BUSINESS_ID;
                            //entity.APP_NOTIFICATIONS_NAME = obj.APP_NOTIFICATIONS_NAME;
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

        public List<APP_NOTIFICATIONS> GetAll()
        {
            FormattableString sql = $@"select * from APP_NOTIFICATIONS ORDER BY CREATE_DATE DESC";
            return dbCtx.Database.SqlQuery<APP_NOTIFICATIONS>(sql).ToList();
        }
        public List<APP_NOTIFICATIONS> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM APP_NOTIFICATIONS BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.APP_NOTIFICATIONS_NAME";
            return dbCtx.Database.SqlQuery<APP_NOTIFICATIONS>(sql).ToList();
        }
        public APP_NOTIFICATIONS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM APP_NOTIFICATIONS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<APP_NOTIFICATIONS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "APP_NOTIFICATIONS";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.BANK_APP_NOTIFICATIONS.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.APP_NOTIFICATIONS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.APP_NOTIFICATIONS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.TITLE_TEXT!);
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
