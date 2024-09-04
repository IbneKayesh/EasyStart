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

                            entity.BUSINESS_LINE_ID = obj.BUSINESS_LINE_ID;
                            entity.PRODUCT_TYPE_ID = obj.PRODUCT_TYPE_ID;
                            entity.PRODUCT_CLASS_ID = obj.PRODUCT_CLASS_ID;
                            entity.PRODUCT_CATEGORY_ID = obj.PRODUCT_CATEGORY_ID;
                            entity.UNIT_CHILD_ID = obj.UNIT_CHILD_ID;
                            entity.PRODUCT_CODE = obj.PRODUCT_CODE;
                            entity.BAR_CODE = obj.BAR_CODE;
                            entity.PRODUCT_NAME = obj.PRODUCT_NAME;
                            entity.PRODUCT_DESC = obj.PRODUCT_DESC;
                            entity.LAST_PURCHASE_RATE = obj.LAST_PURCHASE_RATE;
                            entity.LAST_SALES_RATE = obj.LAST_SALES_RATE;
                            entity.HAS_WARRANTY = obj.HAS_WARRANTY;
                            entity.HAS_EXPIRY = obj.HAS_EXPIRY;
                            entity.IS_MAIN_PRODUCT = obj.IS_MAIN_PRODUCT;
                            entity.VAT_PCT = obj.VAT_PCT;
                            entity.BASE_PRICE = obj.BASE_PRICE;
                            entity.WEIGHT_PER_UNIT = obj.WEIGHT_PER_UNIT;

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

        public List<PRODUCTS_VM> GetAll(PRODUCTS_IDX obj)
        {
            var conditions = new List<string>();
            List<SqlParameter> param = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(obj.type_name))
            {
                conditions.Add("P.PRODUCT_NAME LIKE @PRODUCT_NAME");
                param.Add(new SqlParameter("@PRODUCT_NAME", "%" + obj.type_name + "%"));
            }
            if (!string.IsNullOrWhiteSpace(obj.class_name))
            {
                conditions.Add("PC.CLASS_NAME LIKE @CLASS_NAME");
                param.Add(new SqlParameter("@CLASS_NAME", "%" + obj.class_name + "%"));
            }

            if (!string.IsNullOrWhiteSpace(obj.category_name))
            {
                conditions.Add("PCT.CATEGORY_NAME LIKE @CATEGORY_NAME");
                param.Add(new SqlParameter("@CATEGORY_NAME", "%" + obj.category_name + "%"));
            }

            if (!string.IsNullOrWhiteSpace(obj.category_name))
            {
                conditions.Add("PCT.CATEGORY_NAME LIKE @CATEGORY_NAME");
                param.Add(new SqlParameter("@CATEGORY_NAME", "%" + obj.category_name + "%"));
            }

            if (!string.IsNullOrWhiteSpace(obj.product_name))
            {
                conditions.Add("P.PRODUCT_NAME LIKE @PRODUCT_NAME");
                param.Add(new SqlParameter("@PRODUCT_NAME", "%" + obj.product_name + "%"));
            }
            string criteria = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            string sql = $@"Select P.*,BL.BUSINESS_LINE_NAME,PT.TYPE_NAME, PC.CLASS_NAME, PCT.CATEGORY_NAME, UC.UNIT_NAME
from PRODUCTS P
Join BUSINESS_LINE BL on P.BUSINESS_LINE_ID = BL.ID
Join PRODUCT_TYPE PT on P.PRODUCT_TYPE_ID = PT.ID
Join PRODUCT_CLASS PC on P.PRODUCT_CLASS_ID = PC.ID
Join PRODUCT_CATEGORY PCT on P.PRODUCT_CATEGORY_ID = PCT.ID
Join UNIT_CHILD UC on P.UNIT_CHILD_ID = UC.ID {criteria}
                            ORDER BY BL.BUSINESS_LINE_NAME,PT.TYPE_NAME, PC.CLASS_NAME, PCT.CATEGORY_NAME, P.PRODUCT_NAME";
            return dbCtx.Database.SqlQueryRaw<PRODUCTS_VM>(sql, param.ToArray()).ToList();
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


        public List<PRODUCTS_VM> GetProductsForSalesBookingByName(string productName)
        {
            List<object> param = new List<object>();
            param.Add(new SqlParameter("@productName", "%" + productName + "%"));
            string sql = @"Select P.*,BL.BUSINESS_LINE_NAME,PT.TYPE_NAME, PC.CLASS_NAME, PCT.CATEGORY_NAME, UC.UNIT_NAME
from PRODUCTS P
Join BUSINESS_LINE BL on P.BUSINESS_LINE_ID = BL.ID
Join PRODUCT_TYPE PT on P.PRODUCT_TYPE_ID = PT.ID
Join PRODUCT_CLASS PC on P.PRODUCT_CLASS_ID = PC.ID AND PC.IS_ACTIVE = 1 and PC.IS_SALES = 1
Join PRODUCT_CATEGORY PCT on P.PRODUCT_CATEGORY_ID = PCT.ID
Join UNIT_CHILD UC on P.UNIT_CHILD_ID = UC.ID WHERE P.IS_ACTIVE = 1 AND P.PRODUCT_NAME LIKE @productName
                            ORDER BY BL.BUSINESS_LINE_NAME,PT.TYPE_NAME, PC.CLASS_NAME, PCT.CATEGORY_NAME, P.PRODUCT_NAME";
            return dbCtx.Database.SqlQueryRaw<PRODUCTS_VM>(sql, param.ToArray()).ToList();
        }

    }
}
