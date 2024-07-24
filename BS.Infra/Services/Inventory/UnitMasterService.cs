namespace BS.Infra.Services.Inventory
{
    public class UnitMasterService
    {
        private readonly AppDbContext dbCtx;
        public UnitMasterService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(UNIT_MASTER obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "UNIT_MASTER";
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

                    dbCtx.UNIT_MASTER.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.UNIT_MASTER.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.UNIT_MASTER_NAME = obj.UNIT_MASTER_NAME;
                            entity.UNIT_MASTER_SHORT_NAME = obj.UNIT_MASTER_SHORT_NAME;
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

        public List<UNIT_MASTER> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM UNIT_MASTER BI ORDER BY BI.UNIT_MASTER_NAME";
            return dbCtx.Database.SqlQuery<UNIT_MASTER>(sql).ToList();
        }
        public List<UNIT_MASTER> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM UNIT_MASTER BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.UNIT_MASTER_NAME";
            return dbCtx.Database.SqlQuery<UNIT_MASTER>(sql).ToList();
        }
        public UNIT_MASTER GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM UNIT_MASTER BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<UNIT_MASTER>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "UNIT_MASTER";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.UNIT_MASTER.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.UNIT_MASTER.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.UNIT_MASTER_NAME!);
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
