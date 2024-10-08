﻿namespace BS.Infra.Services.Inventory
{
    public class ItemAttributeService
    {
        private readonly AppDbContext dbCtx;
        public ItemAttributeService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }

        public EQResult Insert(ITEM_ATTRIBUTE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_ATTRIBUTE";
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

                    dbCtx.ITEM_ATTRIBUTE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.ITEM_ATTRIBUTE.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.ITEM_ATTRIBUTE_SHORT_NAME = obj.ITEM_ATTRIBUTE_SHORT_NAME;
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

        public List<ITEM_ATTRIBUTE> GetAll()
        {
            FormattableString sql = $@"select * from ITEM_ATTRIBUTE order by ITEM_ATTRIBUTE_NAME";
            return dbCtx.Database.SqlQuery<ITEM_ATTRIBUTE>(sql).ToList();
        }
        public List<ITEM_ATTRIBUTE> GetAllActive()
        {
            FormattableString sql = $@"select * from ITEM_ATTRIBUTE WHERE IS_ACTIVE = 1 order by ITEM_ATTRIBUTE_NAME";
            return dbCtx.Database.SqlQuery<ITEM_ATTRIBUTE>(sql).ToList();
        }
        public ITEM_ATTRIBUTE GetById(string id)
        {
            FormattableString sql = $@"Select * from ITEM_ATTRIBUTE WHERE ID = {id}";
            return dbCtx.Database.SqlQuery<ITEM_ATTRIBUTE>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_ATTRIBUTE";
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
                var entity = dbCtx.ITEM_ATTRIBUTE.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.ITEM_ATTRIBUTE.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.ITEM_ATTRIBUTE_NAME!);
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


//        public List<ITEM_ATTRIBUTE_VM> GetSetupBySubGroupId(string item_sub_group_id)
//        {
//            List<object> param = new List<object>();
//            param.Add(new SqlParameter("@ITEM_SUB_GROUP_ID", item_sub_group_id));
//            string sql = $@"SELECT IA.ITEM_ATTRIBUTE_NAME,IA.ITEM_ATTRIBUTE_SHORT_NAME,
//IST.ATTRIBUTE_SEQ, IST.DEFAULT_VALUE,IST.ADD_TO_NAME
//FROM ITEM_SETUP IST
//JOIN ITEM_ATTRIBUTE IA ON IST.ITEM_ATTRIBUTE_ID = IA.ID
//WHERE IST.ITEM_SUB_GROUP_ID = @ITEM_SUB_GROUP_ID
//ORDER BY IST.ATTRIBUTE_SEQ";
//            return dbCtx.Database.SqlQueryRaw<ITEM_ATTRIBUTE_VM>(sql, param.ToArray()).ToList();
//        }
    }
}
