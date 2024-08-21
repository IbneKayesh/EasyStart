using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BS.Infra.Services.Setup
{
    public class EntityValueTextService
    {
        private readonly AppDbContext dbCtx;
        public EntityValueTextService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(ENTITY_VALUE_TEXT obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ENTITY_VALUE_TEXT";
            try
            {
                //old entity
                var entity = dbCtx.ENTITY_VALUE_TEXT.Find(obj.VALUE_ID);
                if (entity != null)
                {
                    if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                    {
                        //TODO : Update property
                        entity.VALUE_NAME = obj.VALUE_NAME;
                        entity.ENTITY_ID = obj.ENTITY_ID;
                        entity.ENTITY_DESCRIPTION = obj.ENTITY_DESCRIPTION;
                        entity.IS_DEFAULT = obj.IS_DEFAULT;
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
                    //new entity

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.ENTITY_VALUE_TEXT.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
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

        public List<ENTITY_VALUE_TEXT> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ENTITY_VALUE_TEXT BI
                    ORDER BY BI.ENTITY_ID, BI.VALUE_ID";
            return dbCtx.Database.SqlQuery<ENTITY_VALUE_TEXT>(sql).ToList();
        }
        public List<ENTITY_VALUE_TEXT> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ENTITY_VALUE_TEXT BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.ENTITY_ID";
            return dbCtx.Database.SqlQuery<ENTITY_VALUE_TEXT>(sql).ToList();
        }
        public ENTITY_VALUE_TEXT GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ENTITY_VALUE_TEXT BI
                    WHERE BI.VALUE_ID = {id}";
            return dbCtx.Database.SqlQuery<ENTITY_VALUE_TEXT>(sql).ToList().FirstOrDefault();
        }
        public List<ENTITY_VALUE_TEXT> GetListByEntityID(string entityId)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ENTITY_VALUE_TEXT BI WHERE BI.ENTITY_ID = {entityId} order by VALUE_ID";
            return dbCtx.Database.SqlQuery<ENTITY_VALUE_TEXT>(sql).ToList();
        }

        // Chat GPT Result
        public List<ENTITY_VALUE_TEXT> GetListByEntityID(List<string> entityIds)
        {
            string sql = $@"SELECT BI.*
        FROM ENTITY_VALUE_TEXT BI
        WHERE BI.ENTITY_ID IN ({string.Join(",", entityIds.Select(id => $"@p{id}"))})
        ORDER BY VALUE_ID";

            object[] parameters = entityIds.Select((id, index) => new SqlParameter($"p{id}", id)).ToArray();
            return dbCtx.Database.SqlQueryRaw<ENTITY_VALUE_TEXT>(sql, parameters).ToList();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ENTITY_VALUE_TEXT";
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
                var entity = dbCtx.ENTITY_VALUE_TEXT.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.ENTITY_VALUE_TEXT.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.VALUE_NAME!);
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
