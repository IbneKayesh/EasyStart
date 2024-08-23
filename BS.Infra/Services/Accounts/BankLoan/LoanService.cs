using BS.DMO.Models.Accounts.BankLoan;

namespace BS.Infra.Services.Accounts.BankLoan
{
    public class LoanService
    {
        private readonly AppDbContext dbCtx;
        public LoanService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(BANK_LOAN_MASTER obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BANK_LOAN_MASTER,BANK_LOAN_SCHEDULE";
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

                    dbCtx.BANK_LOAN_MASTER.Add(obj);
                    foreach (var item in obj.BANK_LOAN_SCHEDULE)
                    {

                        //new entity
                        item.ID = Guid.NewGuid().ToString();
                        item.MASTER_ID = obj.ID;

                        //Start Audit
                        //item.IS_ACTIVE = true;
                        item.CREATE_USER = userId;
                        item.CREATE_DATE = DateTime.Now;
                        item.UPDATE_USER = userId;
                        item.UPDATE_DATE = DateTime.Now;
                        //item.REVISE_NO = 0;
                        //End Audit
                    }
                    dbCtx.BANK_LOAN_SCHEDULE.AddRange(obj.BANK_LOAN_SCHEDULE);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.BRANCH.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
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


    }
}
