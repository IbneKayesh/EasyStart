using BS.DMO.Models.HelpDesk;
using BS.DMO.ViewModels.Accounts.BankLoan;
using Microsoft.EntityFrameworkCore;

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
                        //List<BANK_LOAN_SCHEDULE> blsList = new List<BANK_LOAN_SCHEDULE>();
                        foreach (var item in obj.BANK_LOAN_SCHEDULE)
                        {
                            //BANK_LOAN_SCHEDULE bank_loan_schedule = new BANK_LOAN_SCHEDULE();
                            //ObjectMappingHelper.MapProperties<BANK_LOAN_SCHEDULE_VM, BANK_LOAN_SCHEDULE>(item, bank_loan_schedule);

                            //new entity
                            item.ID = Guid.NewGuid().ToString();
                            item.BANK_LOAN_MASTER_ID = obj.BANK_LOAN_MASTER.ID;

                            //Start Audit
                            //item.IS_ACTIVE = true;
                            item.CREATE_USER = userId;
                            item.CREATE_DATE = dateTime;
                            item.UPDATE_USER = userId;
                            item.UPDATE_DATE = dateTime;
                            //item.REVISE_NO = 0;
                            //End Audit
                            //blsList.Add(bank_loan_schedule);
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

                                var entityList = dbCtx.BANK_LOAN_SCHEDULE.Where(x => x.BANK_LOAN_MASTER_ID == obj.BANK_LOAN_MASTER.ID).ToList();
                                dbCtx.BANK_LOAN_SCHEDULE.RemoveRange(entityList);

                                //List<BANK_LOAN_SCHEDULE> blsList = new List<BANK_LOAN_SCHEDULE>();
                                foreach (var item in obj.BANK_LOAN_SCHEDULE)
                                {
                                    //BANK_LOAN_SCHEDULE bank_loan_schedule = new BANK_LOAN_SCHEDULE();
                                    //ObjectMappingHelper.MapProperties<BANK_LOAN_SCHEDULE_VM, BANK_LOAN_SCHEDULE>(item, bank_loan_schedule);

                                    //new entity
                                    item.ID = Guid.NewGuid().ToString();
                                    item.BANK_LOAN_MASTER_ID = obj.BANK_LOAN_MASTER.ID;

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

        public List<BANK_LOAN_MASTER_VM> GetAll()
        {
            string sql = $@"SELECT BLM.*,BCC.COST_CENTER_NAME
                    FROM BANK_LOAN_MASTER BLM
                    JOIN BRANCH_COST_CENTER BCC ON BLM.BRANCH_COST_CENTER_ID = BCC.ID
                    ORDER BY BLM.START_DATE DESC";
            return dbCtx.Database.SqlQueryRaw<BANK_LOAN_MASTER_VM>(sql).ToList();
        }
        public NEW_BANK_LOAN_MASTER_VM GetById(string id)
        {
            List<object> param = new List<object>();
            param.Add(new SqlParameter(parameterName: "MASTER_ID", id));
            var obj = new NEW_BANK_LOAN_MASTER_VM();
            obj.BANK_LOAN_MASTER = new BANK_LOAN_MASTER();
            obj.BANK_LOAN_SCHEDULE = new List<BANK_LOAN_SCHEDULE>();

            string sql = $@"SELECT BI.*
                    FROM BANK_LOAN_MASTER BI
                    WHERE BI.ID = @MASTER_ID";
            obj.BANK_LOAN_MASTER = dbCtx.Database.SqlQueryRaw<BANK_LOAN_MASTER>(sql, param.ToArray()).ToList().FirstOrDefault();
            sql = $@"SELECT *
                    FROM BANK_LOAN_SCHEDULE BLS
                    WHERE BLS.BANK_LOAN_MASTER_ID = @MASTER_ID ORDER BY BLS.SCHEDULE_NO";

            obj.BANK_LOAN_SCHEDULE = dbCtx.Database.SqlQueryRaw<BANK_LOAN_SCHEDULE>(sql, param.ToArray()).ToList();
            obj.BANK_LOAN_MASTER.ALLOW_EDIT = false;
            return obj;
        }

        public BANK_LOAN_SCHEDULE GetByScheduleId(string id)
        {
            List<object> param = new List<object>();
            param.Add(new SqlParameter(parameterName: "ID", id));
            var sql = $@"SELECT * FROM BANK_LOAN_SCHEDULE WHERE ID = @ID";
            var scheduleEntity = dbCtx.Database.SqlQueryRaw<BANK_LOAN_SCHEDULE>(sql, param.ToArray()).FirstOrDefault();

            if (scheduleEntity != null)
            {
                scheduleEntity.PAYMENT_INFO = scheduleEntity.SCHEDULE_NO + " payment";
                scheduleEntity.PAY_AMOUNT = scheduleEntity.TOTAL_AMOUNT;
                scheduleEntity.PAY_DATE = DateTime.Now;
                return scheduleEntity;
            }
            else
            {
                return new BANK_LOAN_SCHEDULE();
            }
        }

        public EQResult InsertPayment(BANK_LOAN_SCHEDULE obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "BANK_LOAN_SCHEDULE";
            try
            {
                string total_due = $@"WITH ScheduleSums AS (
    SELECT
        BANK_LOAN_MASTER_ID,
        COALESCE(SUM(PAY_AMOUNT), 0) AS TotalPayAmount,
        COALESCE(SUM(FINE_AMOUNT), 0) AS TotalFineAmount
    FROM BANK_LOAN_SCHEDULE
	WHERE BANK_LOAN_SCHEDULE.BANK_LOAN_MASTER_ID = '{obj.BANK_LOAN_MASTER_ID}'
    GROUP BY BANK_LOAN_MASTER_ID
)
UPDATE BANK_LOAN_MASTER
SET
    DUE_AMOUNT = TOTAL_AMOUNT - COALESCE(ss.TotalPayAmount, 0),
    FINE_AMOUNT = COALESCE(ss.TotalFineAmount, 0)
FROM ScheduleSums ss
WHERE BANK_LOAN_MASTER.ID = ss.BANK_LOAN_MASTER_ID";
                //        string total_due = $@"UPDATE BANK_LOAN_MASTER
                //SET DUE_AMOUNT = TOTAL_AMOUNT - (
                //    SELECT COALESCE(SUM(BLP.PAY_AMOUNT), 0)
                //    FROM BANK_LOAN_SCHEDULE BLS
                //    INNER JOIN BANK_LOAN_PAYMENTS BLP ON BLS.ID = BLP.BANK_LOAN_SCHEDULE_ID
                //    WHERE BLS.BANK_LOAN_MASTER_ID = BANK_LOAN_MASTER.ID )
                //WHERE ID = (
                //    SELECT BLS.BANK_LOAN_MASTER_ID
                //    FROM BANK_LOAN_SCHEDULE BLS
                //    WHERE BLS.ID = '{obj.BANK_LOAN_SCHEDULE_ID}')";

                //old entity
                var entity = dbCtx.BANK_LOAN_SCHEDULE.Find(obj.ID);
                if (entity != null)
                {
                    if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                    {
                        //TODO : Update property
                        entity.PAYMENT_INFO = obj.PAYMENT_INFO;
                        entity.PAY_AMOUNT = obj.PAY_AMOUNT;
                        entity.PAY_DATE = obj.PAY_DATE;
                        entity.FINE_AMOUNT = obj.FINE_AMOUNT;
                        entity.FINE_DATE = obj.FINE_DATE;
                        //Start Audit
                        entity.IS_ACTIVE = obj.IS_ACTIVE;
                        entity.UPDATE_USER = userId;
                        entity.UPDATE_DATE = dateTime;
                        entity.REVISE_NO = entity.REVISE_NO + 1;
                        //End Audit
                        dbCtx.Entry(entity).State = EntityState.Modified;
                        eQResult.rows = dbCtx.SaveChanges();
                        eQResult.success = true;
                        eQResult.messages = NotifyService.EditSuccess();
                        dbCtx.Database.ExecuteSqlRaw(total_due);
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

                    eQResult.messages = NotifyService.NotFoundString();
                    eQResult.success = false;
                    return eQResult;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.Contains("See the inner exception for details")
                                ? ex.InnerException?.Message ?? ex.Message
                                : ex.Message;
                error = error.Replace("'", "");
                //eQResult.messages = NotifyService.Error(error);
                eQResult.messages = error;
                eQResult.success = false;
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }



        public EQResult Delete(string id, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BANK_LOAN_MASTER";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                int anyChild = dbCtx.BANK_LOAN_MASTER.Where(x => x.ID == id && x.IS_APPROVE).Count();
                if (anyChild > 0)
                {
                    eQResult.messages = NotifyService.DeleteHasChildString("Already approve", anyChild, "Loan");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.BANK_LOAN_MASTER.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.BANK_LOAN_MASTER.Remove(entity);
                    dbCtx.BANK_LOAN_SCHEDULE.RemoveRange(dbCtx.BANK_LOAN_SCHEDULE.Where(x => x.BANK_LOAN_MASTER_ID == id));
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.TRN_NO!);
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
