using BS.Infra.Services.Utility;

namespace BS.Infra.Services.Setup
{
    public class CurrencyInfoService
    {
        private readonly AppDbContext dbCtx;
        public CurrencyInfoService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(CURRENCY_INFO obj, string userId)
        {
            bool hasBaseCurrency = dbCtx.CURRENCY_INFO.Where(x => x.IS_BASE_CURRENCY).Any();

            EQResult eQResult = new EQResult();
            eQResult.entities = "CURRENCY_INFO";
            try
            {
                if (obj.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.ID = Guid.NewGuid().ToString();
                    obj.IS_BASE_CURRENCY = !hasBaseCurrency;

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.CURRENCY_INFO.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.CURRENCY_INFO.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.COUNTRY_ID = obj.COUNTRY_ID;
                            entity.CURRENCY_NAME = obj.CURRENCY_NAME;
                            entity.CURRENCY_SIGN = obj.CURRENCY_SIGN;
                            entity.CURRENCY_DESC = obj.CURRENCY_DESC;
                            entity.IS_BASE_CURRENCY = !hasBaseCurrency;
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

        public List<CURRENCY_INFO_VM> GetAll()
        {
            FormattableString sql = $@"SELECT CI.*, C.COUNTRY_NAME
                    FROM CURRENCY_INFO CI
                    JOIN COUNTRY_INFO C ON CI.COUNTRY_ID = C.ID
                    ORDER BY CI.CURRENCY_NAME";

            // var parameters = new { BankID = bankId };

            return dbCtx.Database.SqlQuery<CURRENCY_INFO_VM>(sql).ToList();
        }
        public List<CURRENCY_INFO> GetAllActive()
        {
            FormattableString sql = $@"SELECT CI.*
                    FROM CURRENCY_INFO CI
                    WHERE CI.IS_ACTIVE = 1
                    ORDER BY CI.CURRENCY_NAME";
            return dbCtx.Database.SqlQuery<CURRENCY_INFO>(sql).ToList();
        }
        public CURRENCY_INFO GetById(string id)
        {
            FormattableString sql = $@"SELECT CI.*
                    FROM CURRENCY_INFO CI
                    WHERE CI.ID = {id}";
            return dbCtx.Database.SqlQuery<CURRENCY_INFO>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "CURRENCY_INFO";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                int anyChild = dbCtx.CURRENCY_CONV_RATE.Where(x => x.CURRENCY_ID == id).Count();
                if (anyChild > 0)
                {
                    eQResult.messages = NotifyService.DeleteHasChildString("Conversion Rate", anyChild, "Currency");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.CURRENCY_INFO.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.CURRENCY_INFO.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.CURRENCY_NAME);
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
