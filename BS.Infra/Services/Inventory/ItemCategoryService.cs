﻿using BS.DMO.Models.Inventory;
using BS.Infra.IRepository;

namespace BS.Infra.Services.Inventory
{
    public class ItemCategoryService
    {
        private readonly AppDbContext dbCtx;
        public ItemCategoryService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(ITEM_CATEGORY obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_CATEGORY";
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

                    dbCtx.ITEM_CATEGORY.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.ITEM_CATEGORY.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.CATEGORY_NAME = obj.CATEGORY_NAME;
                            entity.CATEGORY_DESC = obj.CATEGORY_DESC;
                            entity.STOP_PURCHASE = obj.STOP_PURCHASE;
                            entity.STOP_SALES = obj.STOP_SALES;
                            entity.STOP_DISCOUNT = obj.STOP_DISCOUNT;
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

        public List<ITEM_CATEGORY> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                            FROM ITEM_CATEGORY BI
                            ORDER BY BI.CATEGORY_NAME";
            return dbCtx.Database.SqlQuery<ITEM_CATEGORY>(sql).ToList();
        }
        public List<ITEM_CATEGORY> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ITEM_CATEGORY BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.CATEGORY_NAME";
            return dbCtx.Database.SqlQuery<ITEM_CATEGORY>(sql).ToList();
        }
        public ITEM_CATEGORY GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ITEM_CATEGORY BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<ITEM_CATEGORY>(sql).ToList().FirstOrDefault();
        }
        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_CATEGORY";
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
                var entity = dbCtx.ITEM_CATEGORY.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.ITEM_CATEGORY.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.CATEGORY_NAME!);
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
