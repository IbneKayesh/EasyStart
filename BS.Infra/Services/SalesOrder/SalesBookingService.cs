using BS.DMO.StaticValues;

namespace BS.Infra.Services.SalesOrder
{
    public class SalesBookingService
    {
        private readonly AppDbContext dbCtx;
        public SalesBookingService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public NEW_SB_VM NewSalesBooking(string userId, string userName)
        {
            var obj = new NEW_SB_VM();
            obj.SB_MASTER = new SB_MASTER
            {
                TRN_ID = TransactionID.SB,
                TRN_NO = "SB-0001",
                REF_TRN_ID = "",
                TRN_DATE = DateTime.Now,
                FROM_USER_ID = userId,
                TO_USER_ID = userName,
                TRN_VALID_DAYS = 90,



                //TRN_ID = ENUM_TRN_ID.PO.ToString(),
                //TRN_NO = TransactionNoService.CreateNewTrn(ENUM_TRN_ID.PO),
            };
            obj.SB_CHILD = new List<SB_CHILD>();

            return obj;
        }
        public EQResult Insert(NEW_SB_VM obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "SB_MASTER";
            try
            {
                var TrnAutoStepService = dbCtx.TRN_AUTO_STEP.Where(x => x.TRN_ID == obj.SB_MASTER.TRN_ID && x.IS_ACTIVE).FirstOrDefault();
                if (obj.SB_MASTER.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.SB_MASTER.ID = Guid.NewGuid().ToString();

                    obj.SB_MASTER.IS_POSTED = TrnAutoStepService != null ? TrnAutoStepService.IS_POSTED : false;
                    obj.SB_MASTER.POSTED_USER_ID = TrnAutoStepService != null ? userId : null;
                    obj.SB_MASTER.POSTED_DATE = TrnAutoStepService != null ? dateTime : null;
                    obj.SB_MASTER.POSTED_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                    obj.SB_MASTER.IS_APPROVE = TrnAutoStepService != null ? TrnAutoStepService.IS_APPROVE : false;
                    obj.SB_MASTER.APPROVE_USER_ID = TrnAutoStepService != null ? userId : null;
                    obj.SB_MASTER.APPROVE_DATE = TrnAutoStepService != null ? dateTime : null;
                    obj.SB_MASTER.APPROVE_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.SB_MASTER.CREATE_USER = userId;
                    obj.SB_MASTER.CREATE_DATE = dateTime;
                    obj.SB_MASTER.UPDATE_USER = userId;
                    obj.SB_MASTER.UPDATE_DATE = dateTime;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.SB_MASTER.Add(obj.SB_MASTER);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.SB_MASTER.Find(obj.SB_MASTER.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.SB_MASTER.RowVersion))
                        {
                            //TODO : Update property

                            //entity.BRANCH_ID = obj.BRANCH_ID;
                            //entity.BANK_BRANCH_ID = obj.BANK_BRANCH_ID;
                            //entity.COST_CENTER_NAME = obj.COST_CENTER_NAME;
                            //entity.MAX_BALANCE_LIMIT = obj.MAX_BALANCE_LIMIT;
                            //entity.OPEN_DATE = obj.OPEN_DATE;

                            obj.SB_MASTER.IS_POSTED = TrnAutoStepService != null ? TrnAutoStepService.IS_POSTED : false;
                            obj.SB_MASTER.POSTED_USER_ID = TrnAutoStepService != null ? userId : null;
                            obj.SB_MASTER.POSTED_DATE = TrnAutoStepService != null ? dateTime : null;
                            obj.SB_MASTER.POSTED_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                            obj.SB_MASTER.IS_APPROVE = TrnAutoStepService != null ? TrnAutoStepService.IS_APPROVE : false;
                            obj.SB_MASTER.APPROVE_USER_ID = TrnAutoStepService != null ? userId : null;
                            obj.SB_MASTER.APPROVE_DATE = TrnAutoStepService != null ? dateTime : null;
                            obj.SB_MASTER.APPROVE_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                            //Start Audit
                            entity.IS_ACTIVE = obj.SB_MASTER.IS_ACTIVE;
                            entity.UPDATE_USER = userId;
                            entity.UPDATE_DATE = dateTime;
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
