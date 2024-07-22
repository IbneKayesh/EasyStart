using BS.Infra.Services.Utility;

namespace BS.Infra.Services.Setup
{
    public class CurrencyConvRateService
    {
        private readonly AppDbContext dbCtx;
        public CurrencyConvRateService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(CURRENCY_CONV_RATE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "CURRENCY_CONV_RATE";
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

                    dbCtx.CURRENCY_CONV_RATE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.CURRENCY_CONV_RATE.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property

                            entity.CURRENCY_ID = obj.CURRENCY_ID;
                            entity.MONTH_ID = obj.MONTH_ID;
                            entity.YEAR_ID = obj.YEAR_ID;
                            entity.CONVERSION_RATE = obj.CONVERSION_RATE;

                            //Start Audit
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

        public List<CURRENCY_CONV_RATE_VM> GetAll()
        {
            FormattableString sql = $@"SELECT CC.*, CI.CURRENCY_NAME
                    FROM CURRENCY_CONV_RATE CC
                    JOIN CURRENCY_INFO CI ON CC.CURRENCY_ID = CI.ID
                    ORDER BY CC.CURRENCY_ID";

            // var parameters = new { BankID = bankId };

            return dbCtx.Database.SqlQuery<CURRENCY_CONV_RATE_VM>(sql).ToList();
        }
        public List<CURRENCY_INFO> GetAllActive()
        {
            FormattableString sql = $@"SELECT CI.*
                    FROM CURRENCY_INFO BI
                    WHERE CI.IS_ACTIVE = 1
                    ORDER BY CI.CURRENCY_NAME";
            return dbCtx.Database.SqlQuery<CURRENCY_INFO>(sql).ToList();
        }
        public CURRENCY_CONV_RATE GetById(string id)
        {
            FormattableString sql = $@"SELECT CI.*
                    FROM CURRENCY_CONV_RATE CI
                    WHERE CI.ID = {id}";
            return dbCtx.Database.SqlQuery<CURRENCY_CONV_RATE>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "CURRENCY_CONV_RATE";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.COUNTRY_INFO.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.CURRENCY_CONV_RATE.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.CURRENCY_CONV_RATE.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.CONVERSION_RATE.ToString());
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
