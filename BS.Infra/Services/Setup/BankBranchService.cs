using Microsoft.EntityFrameworkCore;

namespace BS.Infra.Services.Setup
{
    public class BankBranchService
    {
        private readonly AppDbContext dbCtx;
        public BankBranchService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(BANK_BRANCH obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.Entities = "BANK_BRANCH";
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

                    dbCtx.BANK_BRANCH.Add(obj);
                    eQResult.Rows = dbCtx.SaveChanges();
                    eQResult.Success = true;
                    eQResult.Messages = NotifyServices.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.BANK_BRANCH.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.BANK_ID = obj.BANK_ID;
                            entity.BRANCH_NAME = obj.BRANCH_NAME;
                            entity.SHORT_NAME = obj.SHORT_NAME;
                            entity.BRANCH_ADDRESS = obj.BRANCH_ADDRESS;
                            entity.ROUTE_NO = obj.ROUTE_NO;
                            entity.SWIFT_CODE = obj.SWIFT_CODE;
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

        public List<BANK_BRANCH_VM> GetAll()
        {
            FormattableString sql = $@"SELECT BB.*, BI.BANK_NAME
                    FROM BANK_BRANCH BB
                    JOIN BANK_INFO BI ON BB.BANK_ID = BI.ID
                    ORDER BY BI.BANK_NAME";

            // var parameters = new { BankID = bankId };

            return dbCtx.Database.SqlQuery<BANK_BRANCH_VM>(sql).ToList();
        }
        public BANK_BRANCH GetById(string id)
        {
            return dbCtx.BANK_BRANCH.Find(id);
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.Entities = "BANK_BRANCH";
            try
            {
                //check child entity
                //int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.Messages = NotifyServices.DeleteHasChild("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.BANK_BRANCH.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.BANK_BRANCH.Remove(entity);
                    eQResult.Rows = dbCtx.SaveChanges();
                    eQResult.Success = true;
                    eQResult.Messages = NotifyServices.DeletedSuccess(entity.BRANCH_NAME);
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
