namespace BS.Infra.Services.Company
{
    public class BranchCostCenterService
    {
        private readonly AppDbContext dbCtx;
        public BranchCostCenterService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(BRANCH_COST_CENTER obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BRANCH_COST_CENTER";
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

                    dbCtx.BRANCH_COST_CENTER.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.BRANCH_COST_CENTER.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.BRANCH_ID = obj.BRANCH_ID;
                            entity.BANK_BRANCH_ID = obj.BANK_BRANCH_ID;
                            entity.COST_CENTER_NAME = obj.COST_CENTER_NAME;
                            entity.MAX_BALANCE_LIMIT = obj.MAX_BALANCE_LIMIT;
                            entity.OPEN_DATE = obj.OPEN_DATE;
                            entity.IS_DAY_CASH = obj.IS_DAY_CASH;
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

        public List<BRANCH_COST_CENTER_VM> GetAll()
        {
            FormattableString sql = $@"SELECT BCC.*, B.BRANCH_NAME, BB.BRANCH_NAME BANK_BRANCH_NAME
FROM BRANCH_COST_CENTER BCC
JOIN BRANCH B ON BCC.BRANCH_ID = B.ID
JOIN BANK_BRANCH BB ON BCC.BANK_BRANCH_ID = BB.ID";
            return dbCtx.Database.SqlQuery<BRANCH_COST_CENTER_VM>(sql).ToList();
        }
        public List<BRANCH_COST_CENTER> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BRANCH_COST_CENTER BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.COST_CENTER_NAME";
            return dbCtx.Database.SqlQuery<BRANCH_COST_CENTER>(sql).ToList();
        }
        public List<BRANCH_COST_CENTER> GetCashNonCash()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BRANCH_COST_CENTER BI
                    WHERE BI.IS_ACTIVE = 1 AND BI.IS_DAY_CASH = 0
                    ORDER BY BI.COST_CENTER_NAME";
            return dbCtx.Database.SqlQuery<BRANCH_COST_CENTER>(sql).ToList();
        }
        public BRANCH_COST_CENTER GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BRANCH_COST_CENTER BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<BRANCH_COST_CENTER>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BRANCH_COST_CENTER";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.BANK_BRANCH_COST_CENTER.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("BRANCH_COST_CENTER", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.BRANCH_COST_CENTER.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.BRANCH_COST_CENTER.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.COST_CENTER_NAME!);
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
