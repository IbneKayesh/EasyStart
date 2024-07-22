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
            eQResult.entities = "BANK_BRANCH";
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

                    dbCtx.BANK_BRANCH.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
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
                eQResult.messages = NotifyService.Error(ex.Message == string.Empty ? ex.InnerException!.Message : ex.Message);
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
        public List<BANK_BRANCH> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BANK_BRANCH BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.BRANCH_NAME";
            return dbCtx.Database.SqlQuery<BANK_BRANCH>(sql).ToList();
        }
        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BANK_BRANCH";
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
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccess(entity.BRANCH_NAME);
                    return eQResult;
                }
                else
                {
                    eQResult.messages = NotifyService.NotFound();
                    return eQResult;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message == string.Empty ? ex.InnerException.Message : ex.Message;
                eQResult.messages = NotifyService.Error(msg.Replace("'", ""));
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
    }

}
