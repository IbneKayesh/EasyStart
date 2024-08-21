namespace BS.Infra.Services.Inventory
{
    public class ProductSourceService
    {
        private readonly AppDbContext dbCtx;
        public ProductSourceService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(PRODUCT_SOURCE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "PRODUCT_SOURCE";
            try
            {
                //if (obj.DISCOUNT_PCT > 0 && obj.DISCOUNT_VALUE > 0)
                //{
                //    eQResult.messages = NotifyService.Error("Enter Discount % or Value, Both are not allowed for same SOURCE");
                //    return eQResult;
                //}
                //if ((obj.DISCOUNT_PCT > 0 || obj.DISCOUNT_VALUE > 0) && !obj.IS_DISCOUNT)
                //{
                //    eQResult.messages = NotifyService.Error("Make checked Allow Discount");
                //    return eQResult;
                //}
                //if (obj.IS_DISCOUNT && (obj.DISCOUNT_PCT <= 0 || obj.DISCOUNT_VALUE <= 0))
                //{
                //    eQResult.messages = NotifyService.Error("Enter Discount % or Value");
                //    return eQResult;
                //}

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

                    dbCtx.PRODUCT_SOURCE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.PRODUCT_SOURCE.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.SOURCE_NAME = obj.SOURCE_NAME;
                            entity.SOURCE_DESC = obj.SOURCE_DESC;
                            //entity.IS_DISCOUNT = obj.IS_DISCOUNT;
                            //entity.DISCOUNT_PCT = obj.DISCOUNT_PCT;
                            //entity.DISCOUNT_VALUE = obj.DISCOUNT_VALUE;
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

        public List<PRODUCT_SOURCE> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCT_SOURCE BI ORDER BY BI.SOURCE_NAME";
            return dbCtx.Database.SqlQuery<PRODUCT_SOURCE>(sql).ToList();
        }
        public List<PRODUCT_SOURCE> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCT_SOURCE BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.SOURCE_NAME";
            return dbCtx.Database.SqlQuery<PRODUCT_SOURCE>(sql).ToList();
        }
        public PRODUCT_SOURCE GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCT_SOURCE BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<PRODUCT_SOURCE>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "PRODUCT_SOURCE";
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
                var entity = dbCtx.PRODUCT_SOURCE.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.PRODUCT_SOURCE.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.SOURCE_NAME!);
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
