using BS.DMO.Models.SalesOrder;

namespace BS.Infra.Services.SalesOrder
{
    public class SalesBookingService
    {
        private readonly AppDbContext dbCtx;
        public SalesBookingService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public NEW_SB_VM NewSalesBooking()
        {
            var obj = new NEW_SB_VM();
            obj.SB_MASTER = new SB_MASTER
            {
                TRN_SOURCE_ID = "Advance / Ready / Force / Sample / Excess Booking",
                TRN_TYPE_ID = "Local / Export",
                TRN_ID = "SB",
                TRN_NO = "SB-0001",
                REF_TRN_ID = "SB-0001",
                TRN_DATE = DateTime.Now,
                //TRN_ID = ENUM_TRN_ID.PO.ToString(),
                //TRN_NO = TransactionNoService.CreateNewTrn(ENUM_TRN_ID.PO),
            };
            obj.SB_CHILD = new List<SB_CHILD>();

            return obj;
        }
        public EQResult Insert(NEW_SB_VM obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SB_MASTER";
            try
            {
                if (obj.SB_MASTER.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.SB_MASTER.ID = Guid.NewGuid().ToString();

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.SB_MASTER.CREATE_USER = userId;
                    //obj.CREATE_DATE = DateTime.Now;
                    obj.SB_MASTER.UPDATE_USER = userId;
                    //obj.UPDATE_DATE = DateTime.Now;
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
                            //Start Audit
                            entity.IS_ACTIVE = obj.SB_MASTER.IS_ACTIVE;
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
