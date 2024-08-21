using BS.DMO.StaticValues;
using BS.DMO.ViewModels.Inventory;
using BS.Infra.Services.Setup;
using BS.Infra.Services.Utility;

namespace BS.Infra.Services.SalesOrder
{
    public class SalesBookingService
    {
        private readonly AppDbContext dbCtx;
        private readonly TrnLastNoListService trnLastNoListS;
        public SalesBookingService(AppDbContext _dbContext, TrnLastNoListService trnLastNoListService)
        {
            dbCtx = _dbContext;
            trnLastNoListS = trnLastNoListService;
        }
        public NEW_SB_VM NewSalesBooking(string userId, string userName)
        {
            var obj = new NEW_SB_VM();
            obj.SB_MASTER_VM = new SB_MASTER_VM
            {
                TRN_ID = TransactionID.SB,
                TRN_NO = TrnLastNoListService.CreateTransactionNo(TransactionID.SB),
                REF_TRN_ID = "",
                TRN_DATE = DateTime.Now,
                FROM_USER_ID = userId,
                TO_USER_ID = userName,
                TRN_VALID_DAYS = 90,
            };
            obj.SB_CHILD_VM = new List<SB_CHILD_VM>();

            return obj;
        }
        public EQResult Insert(NEW_SB_VM obj, string userId)
        {
            using (var trn = dbCtx.Database.BeginTransaction())
            {
                DateTime dateTime = DateTime.Now;
                EQResult eQResult = new EQResult();
                eQResult.entities = "SB_MASTER";

                SB_MASTER sb_master = new SB_MASTER();
                ObjectMappingHelper.MapProperties<SB_MASTER_VM, SB_MASTER>(obj.SB_MASTER_VM, sb_master);

                try
                {
                    var TrnAutoStepService = dbCtx.TRN_AUTO_STEP.Where(x => x.TRN_ID == obj.SB_MASTER_VM.TRN_ID && x.IS_ACTIVE).FirstOrDefault();
                    if (obj.SB_MASTER_VM.ID == Guid.Empty.ToString())
                    {
                        //new entity
                        sb_master.ID = Guid.NewGuid().ToString();

                        sb_master.TRN_ID = TransactionID.SB;
                        sb_master.TRN_NO = trnLastNoListS.CreateTransactionNo(dbCtx, TransactionID.SB, obj.SB_MASTER_VM.FROM_SUB_SECTION_ID, dateTime);
                        if(sb_master.TRN_NO == null)
                        {
                            eQResult.messages = NotifyService.Error("An error occurred while creating the New transaction");
                            return eQResult;
                        }

                        sb_master.IS_POSTED = TrnAutoStepService != null ? TrnAutoStepService.IS_POSTED : false;
                        sb_master.POSTED_USER_ID = TrnAutoStepService != null ? userId : null;
                        sb_master.POSTED_DATE = TrnAutoStepService != null ? dateTime : null;
                        sb_master.POSTED_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                        sb_master.IS_APPROVE = TrnAutoStepService != null ? TrnAutoStepService.IS_APPROVE : false;
                        sb_master.APPROVE_USER_ID = TrnAutoStepService != null ? userId : null;
                        sb_master.APPROVE_DATE = TrnAutoStepService != null ? dateTime : null;
                        sb_master.APPROVE_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                        //Start Audit
                        //obj.IS_ACTIVE = true;
                        sb_master.CREATE_USER = userId;
                        sb_master.CREATE_DATE = dateTime;
                        sb_master.UPDATE_USER = userId;
                        sb_master.UPDATE_DATE = dateTime;
                        //obj.REVISE_NO = 0;
                        //End Audit

                        dbCtx.SB_MASTER.Add(sb_master);
                        eQResult.rows = dbCtx.SaveChanges();                        
                        eQResult.success = true;
                        eQResult.messages = NotifyService.SaveSuccess();
                    }
                    else
                    {
                        //old entity
                        var entity = dbCtx.SB_MASTER.Find(sb_master.ID);
                        if (entity != null)
                        {
                            if (entity.RowVersion.SequenceEqual(obj.SB_MASTER_VM.RowVersion))
                            {
                                //TODO : Update property

                                //entity.BRANCH_ID = obj.BRANCH_ID;
                                //entity.BANK_BRANCH_ID = obj.BANK_BRANCH_ID;
                                //entity.COST_CENTER_NAME = obj.COST_CENTER_NAME;
                                //entity.MAX_BALANCE_LIMIT = obj.MAX_BALANCE_LIMIT;
                                //entity.OPEN_DATE = obj.OPEN_DATE;

                                entity.IS_POSTED = TrnAutoStepService != null ? TrnAutoStepService.IS_POSTED : false;
                                entity.POSTED_USER_ID = TrnAutoStepService != null ? userId : null;
                                entity.POSTED_DATE = TrnAutoStepService != null ? dateTime : null;
                                entity.POSTED_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                                entity.IS_APPROVE = TrnAutoStepService != null ? TrnAutoStepService.IS_APPROVE : false;
                                entity.APPROVE_USER_ID = TrnAutoStepService != null ? userId : null;
                                entity.APPROVE_DATE = TrnAutoStepService != null ? dateTime : null;
                                entity.APPROVE_NOTE = TrnAutoStepService != null ? "AUTO" : null;

                                //Start Audit
                                entity.IS_ACTIVE = sb_master.IS_ACTIVE;
                                entity.UPDATE_USER = userId;
                                entity.UPDATE_DATE = dateTime;
                                entity.REVISE_NO = entity.REVISE_NO + 1;
                                //End Audit
                                dbCtx.Entry(entity).State = EntityState.Modified;
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



        public List<SB_MASTER> GetAllUnposted()
        {
            FormattableString sql = $@"SELECT SB.* FROM SB_MASTER SB";
            return dbCtx.Database.SqlQuery<SB_MASTER>(sql).ToList();
        }
    }
}
