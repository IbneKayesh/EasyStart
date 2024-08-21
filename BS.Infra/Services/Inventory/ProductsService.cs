namespace BS.Infra.Services.Inventory
{
    public class ProductsService
    {
        private readonly AppDbContext dbCtx;
        public ProductsService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(PRODUCTS obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "PRODUCTS";
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

                    dbCtx.PRODUCTS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.PRODUCTS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            //entity.CATEGORY_NAME = obj.CATEGORY_NAME;
                            //entity.CATEGORY_DESC = obj.CATEGORY_DESC;
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

        public List<PRODUCTS_VM> GetAll()
        {
            FormattableString sql = $@"Select P.*,BL.BUSINESS_LINE_NAME,PT.TYPE_NAME, PC.CLASS_NAME, PCT.CATEGORY_NAME, PB.BRAND_NAME, UC.UNIT_NAME
from PRODUCTS P
Join BUSINESS_LINE BL on P.BUSINESS_LINE_ID = BL.ID
Join PRODUCT_TYPE PT on P.PRODUCT_TYPE_ID = PT.ID
Join PRODUCT_CLASS PC on P.PRODUCT_CLASS_ID = PC.ID
Join PRODUCT_CATEGORY PCT on P.PRODUCT_CATEGORY_ID = PCT.ID
Join PRODUCT_BRAND PB on P.PRODUCT_BRAND_ID =PB.ID
Join UNIT_CHILD UC on P.UNIT_CHILD_ID = UC.ID
                            ORDER BY P.PRODUCT_NAME";
            return dbCtx.Database.SqlQuery<PRODUCTS_VM>(sql).ToList();
        }
        public List<PRODUCTS> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCTS BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.PRODUCT_NAME";
            return dbCtx.Database.SqlQuery<PRODUCTS>(sql).ToList();
        }
        public PRODUCTS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM PRODUCTS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<PRODUCTS>(sql).ToList().FirstOrDefault();
        }
        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "PRODUCTS";
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
                var entity = dbCtx.PRODUCTS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.PRODUCTS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.PRODUCT_NAME!);
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
