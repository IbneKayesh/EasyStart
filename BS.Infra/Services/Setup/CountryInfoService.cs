using BS.Infra.Services.Utility;

namespace BS.Infra.Services.Setup
{
    public class CountryInfoService
    {
        private readonly AppDbContext dbCtx;
        public CountryInfoService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(COUNTRY_INFO obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "COUNTRY_INFO";
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

                    dbCtx.COUNTRY_INFO.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.COUNTRY_INFO.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.COUNTRY_NAME = obj.COUNTRY_NAME;
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

        public List<COUNTRY_INFO> GetAll()
        {
            FormattableString sql = $@"SELECT CI.*
                    FROM COUNTRY_INFO CI
                    ORDER BY CI.COUNTRY_NAME";
            return dbCtx.Database.SqlQuery<COUNTRY_INFO>(sql).ToList();
        }
        public List<COUNTRY_INFO> GetAllActive()
        {
            FormattableString sql = $@"SELECT CI.*
                    FROM COUNTRY_INFO CI
                    WHERE CI.IS_ACTIVE = 1
                    ORDER BY CI.COUNTRY_NAME";
            return dbCtx.Database.SqlQuery<COUNTRY_INFO>(sql).ToList();
        }
        public COUNTRY_INFO GetById(string id)
        {
            FormattableString sql = $@"SELECT CI.*
                    FROM COUNTRY_INFO CI
                    WHERE CI.ID = {id}";
            return dbCtx.Database.SqlQuery<COUNTRY_INFO>(sql).ToList().FirstOrDefault()!;
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "COUNTRY_INFO";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                int anyChild = dbCtx.CURRENCY_INFO.Where(x => x.COUNTRY_ID == id).Count();
                if (anyChild > 0)
                {
                    eQResult.messages = NotifyService.DeleteHasChildString("Currency", anyChild, "Country");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.COUNTRY_INFO.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.COUNTRY_INFO.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.COUNTRY_NAME);
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
