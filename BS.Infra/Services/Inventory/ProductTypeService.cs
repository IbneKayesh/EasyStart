﻿namespace BS.Infra.Services.Inventory
{
    public class ProductTypeService
    {
        private readonly AppDbContext dbCtx;
        public ProductTypeService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(PRODUCT_TYPE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "PRODUCT_TYPE";
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

                    dbCtx.PRODUCT_TYPE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.PRODUCT_TYPE.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.TYPE_NAME = obj.TYPE_NAME;
                            entity.TYPE_DESC = obj.TYPE_DESC;
                            entity.IS_MASTER_PRODUCT = obj.IS_MASTER_PRODUCT;
                            entity.IS_PURCHASE = obj.IS_PURCHASE;
                            entity.IS_SALES = obj.IS_SALES;
                            entity.IS_STOCK = obj.IS_STOCK;
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

        public List<PRODUCT_TYPE> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCT_TYPE BI ORDER BY BI.TYPE_NAME";
            return dbCtx.Database.SqlQuery<PRODUCT_TYPE>(sql).ToList();
        }
        public List<PRODUCT_TYPE> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCT_TYPE BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.TYPE_NAME";
            return dbCtx.Database.SqlQuery<PRODUCT_TYPE>(sql).ToList();
        }
        public PRODUCT_TYPE GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCT_TYPE BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<PRODUCT_TYPE>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "PRODUCT_TYPE";
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
                var entity = dbCtx.PRODUCT_TYPE.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.PRODUCT_TYPE.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.TYPE_NAME!);
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
