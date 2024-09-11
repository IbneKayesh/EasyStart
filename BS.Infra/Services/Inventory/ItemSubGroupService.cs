using BS.DMO.ViewModels.Inventory;

namespace BS.Infra.Services.Inventory
{
    public class ItemSubGroupService
    {
        private readonly AppDbContext dbCtx;
        public ItemSubGroupService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(ITEM_SUB_GROUP obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_SUB_GROUP";
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

                    dbCtx.ITEM_SUB_GROUP.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.ITEM_SUB_GROUP.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.ITEM_GROUP_ID = obj.ITEM_GROUP_ID;
                            entity.ITEM_SUB_GROUP_NAME = obj.ITEM_SUB_GROUP_NAME;
                            entity.ITEM_SUB_GROUP_DESC = obj.ITEM_SUB_GROUP_DESC;
                            entity.IS_FG_RM = obj.IS_FG_RM;
                            entity.IS_STOCK = obj.IS_STOCK;
                            entity.HS_CODE = obj.HS_CODE;
                            entity.CODE_PREFIX = obj.CODE_PREFIX;
                            entity.LEAD_DAYS = obj.LEAD_DAYS;
                            entity.ITEM_SUB_GROUP_IMG = obj.ITEM_SUB_GROUP_IMG;
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

        public List<ITEM_SUB_GROUP_VM> GetAll()
        {
            string sql = @"SELECT ISG.ID,ISG.ITEM_GROUP_ID,ITEM_SUB_GROUP_NAME,ITEM_SUB_GROUP_DESC,IS_FG_RM,
IS_STOCK,HS_CODE,CODE_PREFIX,LEAD_DAYS,ITEM_SUB_GROUP_IMG,ISG.IS_ACTIVE,ISG.CREATE_USER,ISG.CREATE_DATE,ISG.UPDATE_USER,
ISG.UPDATE_DATE,ISG.REVISE_NO,ISG.RowVersion,IG.ITEM_GROUP_NAME,COUNT (IST.ID) ITEM_SETUP_COUNT
FROM ITEM_SUB_GROUP ISG
JOIN ITEM_GROUP IG ON ISG.ITEM_GROUP_ID = IG.ID
LEFT JOIN ITEM_SETUP IST ON ISG.ID = IST.ITEM_SUB_GROUP_ID
GROUP BY ISG.ID,ITEM_GROUP_ID,ITEM_SUB_GROUP_NAME,ITEM_SUB_GROUP_DESC,IS_FG_RM,
IS_STOCK,HS_CODE,CODE_PREFIX,LEAD_DAYS,ITEM_SUB_GROUP_IMG,ISG.IS_ACTIVE,ISG.CREATE_USER,ISG.CREATE_DATE,ISG.UPDATE_USER,
ISG.UPDATE_DATE,ISG.REVISE_NO,ISG.RowVersion,IG.ITEM_GROUP_NAME ORDER BY ISG.ITEM_GROUP_ID,ITEM_SUB_GROUP_NAME";
            return dbCtx.Database.SqlQueryRaw<ITEM_SUB_GROUP_VM>(sql).ToList();
        }
        public List<ITEM_SUB_GROUP> GetAllActive()
        {
            FormattableString sql = $@"select * from ITEM_SUB_GROUP WHERE IS_ACTIVE = 1 order by ITEM_SUB_GROUP_NAME";
            return dbCtx.Database.SqlQuery<ITEM_SUB_GROUP>(sql).ToList();
        }

        public List<ITEM_SUB_GROUP> GetForSales()
        {
            FormattableString sql = $@"select * from ITEM_SUB_GROUP WHERE IS_ACTIVE = 1 AND IS_FG_RM = 1 order by ITEM_SUB_GROUP_NAME";
            return dbCtx.Database.SqlQuery<ITEM_SUB_GROUP>(sql).ToList();
        }
        public ITEM_SUB_GROUP GetById(string id)
        {
            FormattableString sql = $@"Select * from ITEM_SUB_GROUP WHERE ID = {id}";
            return dbCtx.Database.SqlQuery<ITEM_SUB_GROUP>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_SUB_GROUP";
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
                var entity = dbCtx.ITEM_SUB_GROUP.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.ITEM_SUB_GROUP.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.ITEM_SUB_GROUP_NAME!);
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










        public EQResult CreateSetup(ITEM_SETUP obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_SETUP";
            try
            {
                //old entity
                var entity = dbCtx.ITEM_SETUP.Where(x => x.ITEM_SUB_GROUP_ID == obj.ITEM_SUB_GROUP_ID && x.ITEM_ATTRIBUTE_VALUE_ID == obj.ITEM_ATTRIBUTE_VALUE_ID).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                    {
                        //TODO : Update property
                        entity.DEFAULT_VALUE=obj.DEFAULT_VALUE;
                        entity.ADD_TO_NAME = obj.ADD_TO_NAME;
                        //Start Audit
                        entity.IS_ACTIVE = obj.IS_ACTIVE;
                        entity.UPDATE_USER = userId;
                        entity.UPDATE_DATE = DateTime.Now;
                        entity.REVISE_NO = entity.REVISE_NO + 1;
                        //End Audit
                        //dbCtx.Entry(entity).State = EntityState.Modified;
                        //eQResult.rows = dbCtx.SaveChanges();
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
                else if (obj.ID == Guid.Empty.ToString())
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

                    dbCtx.ITEM_SETUP.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //don't update
                    //old entity
                    eQResult.messages = NotifyService.NotFound();
                    return eQResult;
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
        public List<ITEM_SETUP_VM> GetSetupListByItemSubGroupId(string item_sub_group_id)
        {
            List<object> param = new List<object>();
            param.Add(new SqlParameter("@ITEM_SUB_GROUP_ID", item_sub_group_id));
            string sql = @"SELECT ITS.*, IA.ITEM_ATTRIBUTE_NAME
FROM ITEM_SETUP ITS
JOIN ITEM_ATTRIBUTE IA ON ITS.ITEM_ATTRIBUTE_ID = IA.ID
WHERE ITS.ITEM_SUB_GROUP_ID = @ITEM_SUB_GROUP_ID order by ITS.ITEM_ATTRIBUTE_VALUE_ID";
            return dbCtx.Database.SqlQueryRaw<ITEM_SETUP_VM>(sql, param.ToArray()).ToList();
        }
    }
}
