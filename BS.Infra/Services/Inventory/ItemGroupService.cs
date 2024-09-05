namespace BS.Infra.Services.Inventory
{
    public class ItemGroupService
    {
        private readonly AppDbContext dbCtx;
        public ItemGroupService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(ITEM_GROUP obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_GROUP";
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

                    dbCtx.ITEM_GROUP.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.ITEM_GROUP.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //check is it already used or not
                            //TODO : Update property
                            entity.ITEM_GROUP_NAME = obj.ITEM_GROUP_NAME;
                            entity.ITEM_GROUP_DESC = obj.ITEM_GROUP_DESC;
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

        public List<ITEM_GROUP_VM> GetAll()
        {
            FormattableString sql = $@"select IG.*,IGT.ITEM_GROUP_TYPE_NAME
from ITEM_GROUP IG
Join ITEM_GROUP_TYPE IGT ON IG.ITEM_GROUP_TYPE_ID = IGT.ID
order by IGT.ITEM_GROUP_TYPE_NAME,IG.ITEM_GROUP_NAME";
            return dbCtx.Database.SqlQuery<ITEM_GROUP_VM>(sql).ToList();
        }
        public List<ITEM_GROUP> GetAllActive()
        {
            FormattableString sql = $@"select * from ITEM_GROUP WHERE IS_ACTIVE = 1 order by ITEM_GROUP_NAME";
            return dbCtx.Database.SqlQuery<ITEM_GROUP>(sql).ToList();
        }
        public ITEM_GROUP GetById(string id)
        {
            FormattableString sql = $@"Select * from ITEM_GROUP WHERE ID = {id}";
            return dbCtx.Database.SqlQuery<ITEM_GROUP>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_GROUP";
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
                var entity = dbCtx.ITEM_GROUP.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.ITEM_GROUP.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.ITEM_GROUP_NAME!);
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
