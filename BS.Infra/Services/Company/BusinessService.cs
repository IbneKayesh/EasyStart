namespace BS.Infra.Services.Company
{
    public class BusinessService
    {
        private readonly AppDbContext dbCtx;
        public BusinessService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(BUSINESS obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BUSINESS";
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

                    dbCtx.BUSINESS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.BUSINESS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.BUSINESS_NAME = obj.BUSINESS_NAME;
                            entity.SHORT_NAME = obj.SHORT_NAME;
                            entity.ADDRESS_INFO = obj.ADDRESS_INFO;
                            entity.CONTACT_NAME = obj.CONTACT_NAME;
                            entity.CONTACT_NO = obj.CONTACT_NO;
                            entity.EMAIL_ADDRESS = obj.EMAIL_ADDRESS;
                            entity.MAX_EMPLOYEE = obj.MAX_EMPLOYEE;
                            entity.MAX_SALARY = obj.MAX_SALARY;
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

        public List<BUSINESS> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BUSINESS BI
                    ORDER BY BI.BUSINESS_NAME";
            return dbCtx.Database.SqlQuery<BUSINESS>(sql).ToList();
        }
        public List<BUSINESS> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BUSINESS BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.BUSINESS_NAME";
            return dbCtx.Database.SqlQuery<BUSINESS>(sql).ToList();
        }
        public BUSINESS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BUSINESS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<BUSINESS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BUSINESS";
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
                var entity = dbCtx.BUSINESS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.BUSINESS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.BUSINESS_NAME!);
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
