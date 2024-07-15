using BS.Infra.DbHelper;

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
            eQResult.Entities = "BANK_INFO";
            try
            {
                if (obj.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.ID = Guid.NewGuid().ToString();

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    //obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    //obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.BANK_INFO.Add(obj);
                    eQResult.Rows = dbCtx.SaveChanges();
                    eQResult.Success = true;
                    eQResult.Messages = NotifyServices.SaveSuccess();
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
                            entity.UPDATE_USER = userId;
                            entity.UPDATE_DATE = DateTime.Now;
                            entity.REVISE_NO = entity.REVISE_NO + 1;
                            //End Audit
                            dbCtx.Entry(entity).State = EntityState.Modified;
                            eQResult.Rows = dbCtx.SaveChanges();
                            eQResult.Success = true;
                            eQResult.Messages = NotifyServices.EditSuccess();
                            return eQResult;
                        }
                        else
                        {
                            eQResult.Messages = NotifyServices.EditRestricted();
                            return eQResult;
                        }
                    }
                    else
                    {
                        eQResult.Messages = NotifyServices.NotFound();
                        return eQResult;
                    }
                }
            }
            catch (Exception ex)
            {
                eQResult.Messages = NotifyServices.Error(ex.Message == string.Empty ? ex.InnerException.Message : ex.Message);
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }

        public List<BANK_INFO> GetAll()
        {
            return dbCtx.BANK_INFO.OrderBy(x => x.BANK_NAME).ToList();
        }
        public BANK_INFO GetById(string id)
        {
            return dbCtx.BANK_INFO.Find(id);
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.Entities = "BANK_INFO";
            try
            {
                //check child entity
                int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                if (anyChild > 0)
                {
                    eQResult.Messages = NotifyServices.DeleteHasChild("Branch", anyChild, "Bank");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.BANK_INFO.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.BANK_INFO.Remove(entity);
                    eQResult.Rows = dbCtx.SaveChanges();
                    eQResult.Success = true;
                    eQResult.Messages = NotifyServices.DeletedSuccess(entity.BANK_NAME);
                    return eQResult;
                }
                else
                {
                    eQResult.Messages = NotifyServices.NotFound();
                    return eQResult;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message == string.Empty ? ex.InnerException.Message : ex.Message;
                eQResult.Messages = NotifyServices.Error(msg.Replace("'", ""));
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
    }

}
