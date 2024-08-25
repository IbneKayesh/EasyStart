using BS.DMO.Models.Accounts.BankLoan;
using BS.DMO.Models.Inventory.Warehouse;
using BS.DMO.Models.SalesOrder;
using BS.DMO.StaticValues;
using BS.DMO.ViewModels.Accounts.BankLoan;
using BS.Infra.Services.Setup;

namespace BS.Infra.Services.Accounts.BankLoan
{
    public class LoanService
    {
        private readonly AppDbContext dbCtx;
        private readonly TrnLastNoListService trnLastNoListS;
        public LoanService(AppDbContext _dbContext, TrnLastNoListService trnLastNoListService)
        {
            dbCtx = _dbContext;
            trnLastNoListS = trnLastNoListService;
        }
        public NEW_BANK_LOAN_MASTER_VM NewLoanMaster()
        {
            var obj = new NEW_BANK_LOAN_MASTER_VM();
            obj.BANK_LOAN_MASTER = new BANK_LOAN_MASTER();
            obj.BANK_LOAN_MASTER.TRN_NO = TrnLastNoListService.CreateTransactionNo(TransactionID.BLC);
            obj.BANK_LOAN_SCHEDULE = new List<BANK_LOAN_SCHEDULE>();
            return obj;
        }
        public EQResult Insert(NEW_BANK_LOAN_MASTER_VM obj, string userId)
        {
            using (var trn = dbCtx.Database.BeginTransaction())
            {
                DateTime dateTime = DateTime.Now;
                EQResult eQResult = new EQResult();
                eQResult.entities = "BANK_LOAN_MASTER,BANK_LOAN_SCHEDULE";
                try
                {
                    if (obj.BANK_LOAN_MASTER.ID == Guid.Empty.ToString())
                    {
                        //new entity
                        obj.BANK_LOAN_MASTER.ID = Guid.NewGuid().ToString();
                        obj.BANK_LOAN_MASTER.TRN_NO = trnLastNoListS.CreateTransactionNo(dbCtx, TransactionID.BLC, obj.BANK_LOAN_MASTER.BRANCH_COST_CENTER_ID, dateTime, true);
                        if (obj.BANK_LOAN_MASTER.TRN_NO == null)
                        {
                            eQResult.messages = NotifyService.Error("An error occurred while creating the New transaction");
                            return eQResult;
                        }
                        //Start Audit
                        //obj.BANK_LOAN_MASTER.IS_ACTIVE = true;
                        obj.BANK_LOAN_MASTER.CREATE_USER = userId;
                        obj.BANK_LOAN_MASTER.CREATE_DATE = dateTime;
                        obj.BANK_LOAN_MASTER.UPDATE_USER = userId;
                        obj.BANK_LOAN_MASTER.UPDATE_DATE = dateTime;
                        //obj.REVISE_NO = 0;
                        //End Audit

                        dbCtx.BANK_LOAN_MASTER.Add(obj.BANK_LOAN_MASTER);
                        foreach (var item in obj.BANK_LOAN_SCHEDULE)
                        {
                            //new entity
                            item.ID = Guid.NewGuid().ToString();
                            item.MASTER_ID = obj.BANK_LOAN_MASTER.ID;

                            //Start Audit
                            //item.IS_ACTIVE = true;
                            item.CREATE_USER = userId;
                            item.CREATE_DATE = dateTime;
                            item.UPDATE_USER = userId;
                            item.UPDATE_DATE = dateTime;
                            //item.REVISE_NO = 0;
                            //End Audit
                        }
                        dbCtx.BANK_LOAN_SCHEDULE.AddRange(obj.BANK_LOAN_SCHEDULE);
                        eQResult.rows = dbCtx.SaveChanges();
                        eQResult.success = true;
                        eQResult.messages = NotifyService.SaveSuccess();
                    }
                    else
                    {
                        //old entity
                        var entity = dbCtx.BANK_LOAN_MASTER.Find(obj.BANK_LOAN_MASTER.ID);
                        if (entity != null)
                        {
                            if (entity.RowVersion.SequenceEqual(obj.BANK_LOAN_MASTER.RowVersion))
                            {
                                //TODO : Update property
                                entity.BRANCH_COST_CENTER_ID = obj.BANK_LOAN_MASTER.BRANCH_COST_CENTER_ID;
                                entity.START_DATE = obj.BANK_LOAN_MASTER.START_DATE;
                                entity.END_DATE = obj.BANK_LOAN_MASTER.END_DATE;
                                entity.TRN_NOTE = obj.BANK_LOAN_MASTER.TRN_NOTE;
                                entity.LOAN_AMOUNT = obj.BANK_LOAN_MASTER.LOAN_AMOUNT;
                                entity.INTEREST_RATE = obj.BANK_LOAN_MASTER.INTEREST_RATE;
                                entity.TOTAL_AMOUNT = obj.BANK_LOAN_MASTER.TOTAL_AMOUNT;
                                entity.NO_OF_SCHEDULE = obj.BANK_LOAN_MASTER.NO_OF_SCHEDULE;
                                entity.DUE_AMOUNT = obj.BANK_LOAN_MASTER.DUE_AMOUNT;                                
                                //Start Audit
                                entity.IS_ACTIVE = obj.BANK_LOAN_MASTER.IS_ACTIVE;
                                entity.UPDATE_USER = userId;
                                entity.UPDATE_DATE = dateTime;
                                entity.REVISE_NO = entity.REVISE_NO + 1;
                                //End Audit
                                dbCtx.Entry(entity).State = EntityState.Modified;

                                var entityList = dbCtx.BANK_LOAN_SCHEDULE.Where(x => x.MASTER_ID == obj.BANK_LOAN_MASTER.ID).ToList();
                                dbCtx.BANK_LOAN_SCHEDULE.RemoveRange(entityList);

                                foreach (var item in obj.BANK_LOAN_SCHEDULE)
                                {
                                    //new entity
                                    item.ID = Guid.NewGuid().ToString();
                                    item.MASTER_ID = obj.BANK_LOAN_MASTER.ID;

                                    //Start Audit
                                    //item.IS_ACTIVE = true;
                                    item.CREATE_USER = userId;
                                    item.CREATE_DATE = dateTime;
                                    item.UPDATE_USER = userId;
                                    item.UPDATE_DATE = dateTime;
                                    //item.REVISE_NO = 0;
                                    //End Audit
                                }
                                dbCtx.BANK_LOAN_SCHEDULE.AddRange(obj.BANK_LOAN_SCHEDULE);

                                eQResult.rows = dbCtx.SaveChanges();
                                eQResult.success = true;
                                eQResult.messages = NotifyService.EditSuccess();
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
                    trn.Commit();
                    return eQResult;
                }
                catch (Exception ex)
                {
                    trn.Rollback();

                    if (ex.Message == "An error occurred while saving the entity changes. See the inner exception for details.")
                    {
                        eQResult.messages = ex.InnerException.Message;
                    }
                    else
                    {
                        eQResult.messages = ex.Message == string.Empty ? ex.InnerException.Message : ex.Message;
                    }
                    return eQResult;
                }
                finally
                {
                    dbCtx.Dispose();
                }
            }


        }

        public List<BANK_LOAN_MASTER> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BANK_LOAN_MASTER BI
                    ORDER BY BI.START_DATE DESC";
            return dbCtx.Database.SqlQuery<BANK_LOAN_MASTER>(sql).ToList();
        }
        public NEW_BANK_LOAN_MASTER_VM GetById(string id)
        {
            var obj = new NEW_BANK_LOAN_MASTER_VM();
            obj.BANK_LOAN_MASTER = new BANK_LOAN_MASTER();
            obj.BANK_LOAN_SCHEDULE = new List<BANK_LOAN_SCHEDULE>();

            FormattableString sql = $@"SELECT BI.*
                    FROM BANK_LOAN_MASTER BI
                    WHERE BI.ID = {id}";
            obj.BANK_LOAN_MASTER= dbCtx.Database.SqlQuery<BANK_LOAN_MASTER>(sql).ToList().FirstOrDefault();
            sql = $@"SELECT BI.*
                    FROM BANK_LOAN_SCHEDULE BI
                    WHERE BI.MASTER_ID = {id} ORDER BY BI.SCHEDULE_NO";

            obj.BANK_LOAN_SCHEDULE = dbCtx.Database.SqlQuery<BANK_LOAN_SCHEDULE>(sql).ToList();
            return obj;
        }
    }
}
