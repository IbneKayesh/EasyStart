using BS.DMO.ViewModels;
using System.Collections.Generic;

namespace BS.Infra.Services.Inventory
{
    public class ItemMasterService
    {
        private readonly AppDbContext dbCtx;
        public ItemMasterService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(ITEM_MASTER obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_MASTER";
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

                    dbCtx.ITEM_MASTER.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.ITEM_MASTER.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property

                            entity.ID = obj.ID;
                            //entity.ITEM_SUB_GROUP_ID = obj.ITEM_SUB_GROUP_ID;
                            entity.ITEM_CLASS_ID = obj.ITEM_CLASS_ID;
                            entity.ITEM_CATEGORY_ID = obj.ITEM_CATEGORY_ID;
                            entity.ITEM_TYPE_ID = obj.ITEM_TYPE_ID;
                            entity.UNIT_CHILD_ID = obj.UNIT_CHILD_ID;
                            entity.ITEM_STATUS_ID = obj.ITEM_STATUS_ID;
                            entity.ITEM_NAME = obj.ITEM_NAME;
                            entity.WARRANTY_DAYS = obj.WARRANTY_DAYS;
                            entity.EXPIRY_DAYS = obj.EXPIRY_DAYS;
                            entity.IS_MAIN_ITEM = obj.IS_MAIN_ITEM;
                            entity.VAT_PCT = obj.VAT_PCT;
                            entity.BASE_PRICE = obj.BASE_PRICE;
                            entity.ITEM_IMG = obj.ITEM_IMG;
                            entity.SPECIAL_INSTRUCTION = obj.SPECIAL_INSTRUCTION;
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

        public List<ITEM_MASTER_VM> GetAll(ITEM_MASTER_IDX obj)
        {
            var conditions = new List<string>();
            List<SqlParameter> param = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(obj.item_name))
            {
                conditions.Add("IM.ITEM_NAME LIKE @ITEM_NAME");
                param.Add(new SqlParameter("@ITEM_NAME", "%" + obj.item_name + "%"));
            }
            if (!string.IsNullOrWhiteSpace(obj.item_sub_group_name))
            {
                conditions.Add("ISG.ITEM_SUB_GROUP_NAME LIKE @ITEM_SUB_GROUP_NAME");
                param.Add(new SqlParameter("@ITEM_SUB_GROUP_NAME", "%" + obj.item_sub_group_name + "%"));
            }

            if (!string.IsNullOrWhiteSpace(obj.class_name))
            {
                conditions.Add("ICL.CLASS_NAME LIKE @CLASS_NAME");
                param.Add(new SqlParameter("@CLASS_NAME", "%" + obj.class_name + "%"));
            }

            if (!string.IsNullOrWhiteSpace(obj.category_name))
            {
                conditions.Add("ICT.CATEGORY_NAME LIKE @CATEGORY_NAME");
                param.Add(new SqlParameter("@CATEGORY_NAME", "%" + obj.category_name + "%"));
            }

            if (!string.IsNullOrWhiteSpace(obj.item_code))
            {
                conditions.Add("IM.ITEM_CODE LIKE @ITEM_CODE");
                param.Add(new SqlParameter("@ITEM_CODE", "%" + obj.item_code + "%"));
            }
            string criteria = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            string sql = $@"SELECT IM.*,ISG.ITEM_SUB_GROUP_NAME,ICL.CLASS_NAME,ICT.CATEGORY_NAME,ITY.TYPE_NAME,IST.STATUS_NAME,UC.UNIT_NAME
FROM ITEM_MASTER IM
JOIN ITEM_SUB_GROUP ISG ON IM.ITEM_SUB_GROUP_ID = ISG.ID
JOIN ITEM_CLASS ICL ON IM.ITEM_CLASS_ID = ICL.ID
JOIN ITEM_CATEGORY ICT ON IM.ITEM_CATEGORY_ID = ICT.ID
JOIN ITEM_TYPE ITY ON IM.ITEM_TYPE_ID = ITY.ID
JOIN ITEM_STATUS IST ON IM.ITEM_STATUS_ID = IST.ID
JOIN UNIT_CHILD UC ON IM.UNIT_CHILD_ID = UC.ID {criteria} ORDER BY ISG.ITEM_SUB_GROUP_NAME,ICL.CLASS_NAME,ICT.CATEGORY_NAME,ITY.TYPE_NAME,IST.STATUS_NAME,UC.UNIT_NAME";
            return dbCtx.Database.SqlQueryRaw<ITEM_MASTER_VM>(sql, param.ToArray()).ToList();
        }
        public List<ITEM_MASTER> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ITEM_MASTER BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.PRODUCT_NAME";
            return dbCtx.Database.SqlQuery<ITEM_MASTER>(sql).ToList();
        }
        public ITEM_MASTER GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM ITEM_MASTER BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<ITEM_MASTER>(sql).ToList().FirstOrDefault();
        }
        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "ITEM_MASTER";
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
                var entity = dbCtx.ITEM_MASTER.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.ITEM_MASTER.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.ITEM_NAME!);
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


        public GenerateTable GetGenerateTables(string item_sub_group_id)
        {
            GenerateTable generateTable = new GenerateTable();
            generateTable.COLUMN_NAME = "ATTRIBUTE_VALUE1,ATTRIBUTE_VALUE2,ATTRIBUTE_VALUE3,ATTRIBUTE_VALUE4,ATTRIBUTE_VALUE5,ATTRIBUTE_VALUE6,ATTRIBUTE_VALUE7,ATTRIBUTE_VALUE8,ATTRIBUTE_VALUE9,ATTRIBUTE_VALUE10,ATTRIBUTE_VALUE11,ATTRIBUTE_VALUE12,ATTRIBUTE_VALUE13,ATTRIBUTE_VALUE14,ATTRIBUTE_VALUE15";

            List<string> AllCols = ItemMasterAttributeValue.GetAll();

            string colTitles = "ATTRIBUTE_VALUE1,ATTRIBUTE_VALUE2,ATTRIBUTE_VALUE3,ATTRIBUTE_VALUE4,ATTRIBUTE_VALUE5,ATTRIBUTE_VALUE6,ATTRIBUTE_VALUE7,ATTRIBUTE_VALUE8,ATTRIBUTE_VALUE9,ATTRIBUTE_VALUE10,ATTRIBUTE_VALUE11,ATTRIBUTE_VALUE12,ATTRIBUTE_VALUE13,ATTRIBUTE_VALUE14,ATTRIBUTE_VALUE15";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ITEM_SUB_GROUP_ID", item_sub_group_id));
            string sql = @"SELECT IST.ITEM_ATTRIBUTE_VALUE_ID,IA.ITEM_ATTRIBUTE_NAME,IA.ITEM_ATTRIBUTE_SHORT_NAME,
IST.DEFAULT_VALUE,IST.ADD_TO_NAME
FROM ITEM_SETUP IST
JOIN ITEM_ATTRIBUTE IA ON IST.ITEM_ATTRIBUTE_ID = IA.ID
WHERE IST.ITEM_SUB_GROUP_ID = @ITEM_SUB_GROUP_ID
ORDER BY IST.ITEM_ATTRIBUTE_VALUE_ID";
            var attributeData = dbCtx.Database.SqlQueryRaw<ITEM_SETUP_ATTRIBUTE_VM>(sql, param.ToArray());
            foreach (ITEM_SETUP_ATTRIBUTE_VM item in attributeData)
            {
                colTitles = StringModify.ReplaceExact(colTitles, item.ITEM_ATTRIBUTE_VALUE_ID, item.ITEM_ATTRIBUTE_NAME);
                AllCols.Remove(item.ITEM_ATTRIBUTE_VALUE_ID);
            }
            generateTable.COLUMN_TITLE = colTitles;
            generateTable.COLUMN_HIDDEN = string.Join(",", AllCols);
            return generateTable;
        }
        public List<ITEM_DETAILS_VM> GetItemDetailsForSalesBookingBySubGroupIDByItemName(string item_sub_group_id)
        {
            string sql = $@"SELECT IMD.ID,ISG.HS_CODE,ISG.LEAD_DAYS,IM.MASTER_ITEM_CODE,IM.MASTER_BAR_CODE,IM.ITEM_NAME,IM.WARRANTY_DAYS,IM.EXPIRY_DAYS,IM.IS_MAIN_ITEM,
IM.VAT_PCT,IM.BASE_PRICE,IMD.ITEM_CODE,IMD.BAR_CODE,IMD.ITEM_DESC,ATTRIBUTE_VALUE1,ATTRIBUTE_VALUE2,ATTRIBUTE_VALUE3,ATTRIBUTE_VALUE4,ATTRIBUTE_VALUE5,ATTRIBUTE_VALUE6,ATTRIBUTE_VALUE7,ATTRIBUTE_VALUE8,ATTRIBUTE_VALUE9,ATTRIBUTE_VALUE10,ATTRIBUTE_VALUE11,ATTRIBUTE_VALUE12,ATTRIBUTE_VALUE13,ATTRIBUTE_VALUE14,ATTRIBUTE_VALUE15
FROM ITEM_MASTER IM
JOIN ITEM_SUB_GROUP ISG ON IM.ITEM_SUB_GROUP_ID= ISG.ID
LEFT JOIN ITEM_DETAILS IMD ON IM.ID= IMD.ITEM_MASTER_ID
WHERE ISG.IS_FG_RM = 1";
            return dbCtx.Database.SqlQueryRaw<ITEM_DETAILS_VM>(sql).ToList();
        }





        //        public List<ITEM_MASTER_VM> GetProductsForSalesBookingByName(string productName)
        //        {
        //            List<object> param = new List<object>();
        //            param.Add(new SqlParameter("@productName", "%" + productName + "%"));
        //            string sql = @"Select P.*,BL.BUSINESS_LINE_NAME,PT.TYPE_NAME, PC.CLASS_NAME, PCT.CATEGORY_NAME, UC.UNIT_NAME
        //from ITEM_MASTER P
        //Join BUSINESS_LINE BL on P.BUSINESS_LINE_ID = BL.ID
        //Join PRODUCT_TYPE PT on P.PRODUCT_TYPE_ID = PT.ID
        //Join PRODUCT_CLASS PC on P.PRODUCT_CLASS_ID = PC.ID AND PC.IS_ACTIVE = 1 and PC.IS_SALES = 1
        //Join PRODUCT_CATEGORY PCT on P.PRODUCT_CATEGORY_ID = PCT.ID
        //Join UNIT_CHILD UC on P.UNIT_CHILD_ID = UC.ID WHERE P.IS_ACTIVE = 1 AND P.PRODUCT_NAME LIKE @productName
        //                            ORDER BY BL.BUSINESS_LINE_NAME,PT.TYPE_NAME, PC.CLASS_NAME, PCT.CATEGORY_NAME, P.PRODUCT_NAME";
        //            return dbCtx.Database.SqlQueryRaw<ITEM_MASTER_VM>(sql, param.ToArray()).ToList();
        //        }

    }
}
