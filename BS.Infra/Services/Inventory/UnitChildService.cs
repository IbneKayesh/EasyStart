﻿using BS.DMO.ViewModels.Inventory;

namespace BS.Infra.Services.Inventory
{
    public class UnitChildService
    {
        private readonly AppDbContext dbCtx;
        public UnitChildService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(UNIT_CHILD obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "UNIT_CHILD";
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

                    dbCtx.UNIT_CHILD.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.UNIT_CHILD.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.UNIT_MASTER_ID = obj.UNIT_MASTER_ID;
                            entity.UNIT_NAME = obj.UNIT_NAME;
                            entity.SHORT_NAME = obj.SHORT_NAME;
                            entity.RELATIVE_FACTOR = obj.RELATIVE_FACTOR;
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

        public List<UNIT_CHILD_VM> GetAll()
        {
            FormattableString sql = $@"SELECT UC.*,UM.UNIT_MASTER_NAME
                    FROM UNIT_CHILD UC
JOIN UNIT_MASTER UM ON UC.UNIT_MASTER_ID = UM.ID ORDER BY UM.UNIT_MASTER_NAME,UC.UNIT_NAME";
            return dbCtx.Database.SqlQuery<UNIT_CHILD_VM>(sql).ToList();
        }
        public List<UNIT_CHILD> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM UNIT_CHILD BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.UNIT_NAME";
            return dbCtx.Database.SqlQuery<UNIT_CHILD>(sql).ToList();
        }
        public UNIT_CHILD GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM UNIT_CHILD BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<UNIT_CHILD>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "UNIT_CHILD";
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
                var entity = dbCtx.UNIT_CHILD.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.UNIT_CHILD.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.UNIT_NAME!);
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
