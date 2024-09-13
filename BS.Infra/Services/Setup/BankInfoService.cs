namespace BS.Infra.Services.Setup
{
    public class BankInfoService
    {
        private readonly AppDbContext dbCtx;
        public BankInfoService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(BANK_INFO obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BANK_INFO";
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

                    dbCtx.BANK_INFO.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.BANK_INFO.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.BANK_NAME = obj.BANK_NAME;
                            entity.SHORT_NAME = obj.SHORT_NAME;
                            entity.CO_ADDRESS = obj.CO_ADDRESS;
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

        public List<BANK_INFO> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BANK_INFO BI
                    ORDER BY BI.BANK_NAME";
            return dbCtx.Database.SqlQuery<BANK_INFO>(sql).ToList();
        }
        public List<BANK_INFO> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BANK_INFO BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.BANK_NAME";
            return dbCtx.Database.SqlQuery<BANK_INFO>(sql).ToList();
        }
        public BANK_INFO GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BANK_INFO BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<BANK_INFO>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BANK_INFO";
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
                var entity = dbCtx.BANK_INFO.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.BANK_INFO.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.BANK_NAME!);
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
