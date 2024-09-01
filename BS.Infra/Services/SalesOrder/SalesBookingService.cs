namespace BS.Infra.Services.SalesOrder
{
    public class SalesBookingService
    {
        private readonly AppDbContext dbCtx;
        private readonly TrnLastNoListService trnLastNoListS;
        public SalesBookingService(AppDbContext _dbContext, TrnLastNoListService trnLastNoListService)
        {
            dbCtx = _dbContext;
            trnLastNoListS = trnLastNoListService;
        }
        public NEW_SB_VM NewSalesBooking(string userId, string userName)
        {
            var obj = new NEW_SB_VM();
            obj.SB_MASTER_VM = new SB_MASTER_VM
            {
                TRN_ID = TransactionID.SB,
                TRN_NO = TrnLastNoListService.CreateTransactionNo(TransactionID.SB),
                REF_TRN_ID = "",
                TRN_DATE = DateTime.Now,
                FROM_USER_ID = userId,
                TO_USER_ID = userName,
                TRN_VALID_DAYS = 90,
            };
            obj.SB_CHILD_VM = new List<SB_CHILD_VM>();

            return obj;
        }
        public EQResult Insert(NEW_SB_VM obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "SB_MASTER,SB_CHILD";

            SB_MASTER sb_master = new SB_MASTER();
            List<SB_CHILD> sbcList = new List<SB_CHILD>();

            if (obj.SB_CHILD_VM == null || obj.SB_CHILD_VM.Count < 1)
            {
                eQResult.messages = "At least one product information is required";
                return eQResult;
            }
            ObjectMappingHelper.MapProperties<SB_MASTER_VM, SB_MASTER>(obj.SB_MASTER_VM, sb_master);

            //find max last delivery date from details
            List<DateTime> LastDeliveryDateList = new List<DateTime>();
            LastDeliveryDateList.Add(obj.SB_MASTER_VM.LAST_DELIVERY_DATE);
            foreach (var item in obj.SB_CHILD_VM)
            {
                LastDeliveryDateList.Add(item.DELIVERY_DATE ?? obj.SB_MASTER_VM.LAST_DELIVERY_DATE);
            }
            sb_master.LAST_DELIVERY_DATE = DateTimeHelper.FindMaxDate(LastDeliveryDateList);
            //check order date < from the min last delivery date
            DateTime MinDate = DateTimeHelper.FindMinDate(LastDeliveryDateList);
            TimeSpan MinDlvDate_Validation = MinDate - obj.SB_MASTER_VM.TRN_DATE;
            int MINDAYS = MinDlvDate_Validation.Days;
            if (MINDAYS < 1)
            {
                eQResult.messages = "There should not be a negetive between the Order date and Last delivery date";
                return eQResult;
            }
            //SSD
            TimeSpan sdd_Validation_Duration = obj.SB_MASTER_VM.LAST_SDD_DATE - obj.SB_MASTER_VM.TRN_DATE;
            int SDD = sdd_Validation_Duration.Days;
            if (SDD < 0)
            {
                eQResult.messages = "There should not be a negetive between the Order date and Sample delivery date";
                return eQResult;
            }
            sb_master.SDD = SDD;

            //FGD
            TimeSpan fgd_validation = obj.SB_MASTER_VM.LAST_MFG_DATE - obj.SB_MASTER_VM.LAST_SDD_DATE;
            int FGD = fgd_validation.Days;
            if (FGD < 0)
            {
                eQResult.messages = "There should not be a negetive between the Sample delivery date and MFG date";
                return eQResult;
            }
            TimeSpan fgd_duration = obj.SB_MASTER_VM.LAST_MFG_DATE - obj.SB_MASTER_VM.TRN_DATE;
            FGD = fgd_duration.Days;
            sb_master.FGD = FGD;

            //LDD
            TimeSpan ldd_validation = obj.SB_MASTER_VM.LAST_DELIVERY_DATE - obj.SB_MASTER_VM.LAST_MFG_DATE;
            int LDD = ldd_validation.Days;
            if (LDD < 0)
            {
                eQResult.messages = "There should not be a negetive between the MFG date and Last delivery date";
                return eQResult;
            }
            TimeSpan ldd_duration = obj.SB_MASTER_VM.LAST_DELIVERY_DATE - obj.SB_MASTER_VM.TRN_DATE;
            LDD = ldd_duration.Days;
            sb_master.LDD = LDD;

            //disc % and amount both
            bool DiscPctAmount = obj.SB_CHILD_VM.Where(x => x.DISCOUNT_PCT > 0 && x.DISCOUNT_AMOUNT > 0).Any();
            if (DiscPctAmount)
            {
                eQResult.messages = "Discount % and Discount amount both are not allowed for an item";
                return eQResult;
            }
            bool DiscPct100 = obj.SB_CHILD_VM.Where(x => x.DISCOUNT_PCT > 100).Any();
            if (DiscPct100)
            {
                eQResult.messages = "Discount % more than 100 is not allowed for an item";
                return eQResult;
            }
            //total child and master
            var TRN_AMOUNT = 0M;
            var DISCOUNT_AMOUNT = 0M;
            var VAT_AMOUNT = 0M;
            foreach (SB_CHILD_VM item in obj.SB_CHILD_VM)
            {
                var itemAmount = item.PRODUCT_RATE * item.PRODUCT_QTY;
                TRN_AMOUNT += itemAmount;

                var itemVatAmount = 0M;
                if (item.VAT_PCT > 0)
                {
                    itemVatAmount = itemAmount * (item.VAT_PCT / 100);
                }
                VAT_AMOUNT += itemVatAmount;
                var withVAT = itemAmount + itemVatAmount;

                var itemDiscAmount = 0M;
                if (item.DISCOUNT_PCT > 0)
                {
                    itemDiscAmount = withVAT * (item.DISCOUNT_PCT / 100);
                }
                else
                {
                    itemDiscAmount = item.DISCOUNT_AMOUNT;
                }
                DISCOUNT_AMOUNT += itemDiscAmount;
                //assign to item amount
                item.PRODUCT_AMOUNT = withVAT - itemDiscAmount;
            }
            sb_master.TRN_AMOUNT = TRN_AMOUNT;
            sb_master.DISCOUNT_AMOUNT = DISCOUNT_AMOUNT;
            sb_master.VAT_AMOUNT = VAT_AMOUNT;
            sb_master.NET_AMOUNT = (TRN_AMOUNT + VAT_AMOUNT + sb_master.CHARGE_AMOUNT) - (DISCOUNT_AMOUNT + sb_master.ADD_DISCOUNT_AMOUNT);
            //payment
            sb_master.DUE_AMOUNT = sb_master.NET_AMOUNT - (sb_master.ADVANCED_PAYMENT_AMOUNT + sb_master.PAID_AMOUNT);

            //start saving data
            using (var trn = dbCtx.Database.BeginTransaction())
            {
                try
                {
                    var TrnAutoStepService = dbCtx.TRN_AUTO_STEP.Where(x => x.TRN_ID == obj.SB_MASTER_VM.TRN_ID && x.IS_ACTIVE).FirstOrDefault();
                    if (obj.SB_MASTER_VM.ID == Guid.Empty.ToString())
                    {
                        //new entity
                        sb_master.ID = Guid.NewGuid().ToString();

                        sb_master.TRN_ID = TransactionID.SB;
                        sb_master.TRN_NO = trnLastNoListS.CreateTransactionNo(dbCtx, TransactionID.SB, obj.SB_MASTER_VM.FROM_SUB_SECTION_ID, dateTime);
                        if (sb_master.TRN_NO == null)
                        {
                            dbCtx.Dispose();
                            eQResult.messages = "An error occurred while creating the New transaction";
                            return eQResult;
                        }

                        sb_master.REQUIRED_SAMPLE = obj.SB_CHILD_VM.Where(x => x.IS_SAMPLE).Any();

                        bool IsPosted = TrnAutoStepService != null && TrnAutoStepService.IS_POSTED ? TrnAutoStepService.IS_POSTED : obj.SB_MASTER_VM.IS_POSTED;
                        sb_master.IS_POSTED = IsPosted;
                        sb_master.POSTED_USER_ID = IsPosted ? userId : null;
                        sb_master.POSTED_DATE = IsPosted ? dateTime : null;
                        sb_master.POSTED_NOTE = TrnAutoStepService != null && TrnAutoStepService.IS_POSTED ? "AUTO" : (IsPosted ? "Posted" : "");

                        bool IsApprove = TrnAutoStepService != null && TrnAutoStepService.IS_APPROVE ? TrnAutoStepService.IS_APPROVE : obj.SB_MASTER_VM.IS_APPROVE;
                        IsApprove = !IsPosted ? false : IsApprove;
                        sb_master.IS_APPROVE = IsApprove;
                        sb_master.APPROVE_USER_ID = IsApprove ? userId : null;
                        sb_master.APPROVE_DATE = IsApprove ? dateTime : null;
                        sb_master.APPROVE_NOTE = TrnAutoStepService != null && TrnAutoStepService.IS_POSTED ? "AUTO" : (IsApprove ? "Approve" : "");

                        //Start Audit
                        //obj.IS_ACTIVE = true;
                        sb_master.CREATE_USER = userId;
                        sb_master.CREATE_DATE = dateTime;
                        sb_master.UPDATE_USER = userId;
                        sb_master.UPDATE_DATE = dateTime;
                        //obj.REVISE_NO = 0;
                        //End Audit

                        dbCtx.SB_MASTER.Add(sb_master);


                        int i = 0;
                        foreach (var item in obj.SB_CHILD_VM)
                        {
                            i++;
                            SB_CHILD sb_child = new SB_CHILD();
                            ObjectMappingHelper.MapProperties<SB_CHILD_VM, SB_CHILD>(item, sb_child);

                            //new entity
                            sb_child.ID = Guid.NewGuid().ToString();
                            sb_child.MASTER_ID = sb_master.ID;
                            sb_child.LINE_NO = i;
                            sb_child.DELIVERY_DATE = item.DELIVERY_DATE ?? obj.SB_MASTER_VM.LAST_DELIVERY_DATE;
                            sb_child.DELIVERY_ADDRESS_ID = item.DELIVERY_ADDRESS_ID ?? obj.SB_MASTER_VM.DELIVERY_ADDRESS_ID;
                            //Start Audit
                            //item.IS_ACTIVE = true;
                            sb_child.CREATE_USER = userId;
                            sb_child.CREATE_DATE = dateTime;
                            sb_child.UPDATE_USER = userId;
                            sb_child.UPDATE_DATE = dateTime;
                            //item.REVISE_NO = 0;
                            //End Audit
                            sbcList.Add(sb_child);
                        }
                        dbCtx.SB_CHILD.AddRange(sbcList);

                        eQResult.rows = dbCtx.SaveChanges();
                        eQResult.success = true;
                        eQResult.messages = NotifyService.SaveSuccess();
                    }
                    else
                    {
                        //old entity
                        var entity = dbCtx.SB_MASTER.Find(sb_master.ID);
                        if (entity == null)
                        {
                            dbCtx.Dispose();
                            eQResult.messages = NotifyService.NotFound();
                            return eQResult;
                        }
                        if (!entity.RowVersion.SequenceEqual(obj.SB_MASTER_VM.RowVersion))
                        {
                            dbCtx.Dispose();
                            eQResult.messages = NotifyService.EditRestricted();
                            return eQResult;
                        }
                        //TODO : Update property

                        //entity.TRN_SOURCE_ID = obj.TRN_SOURCE_ID;
                        //entity.TRN_TYPE_ID = obj.TRN_TYPE_ID;
                        //entity.TRN_ID = obj.TRN_ID;
                        //entity.TRN_NO = obj.TRN_NO;
                        //entity.REF_TRN_ID = obj.REF_TRN_ID;
                        //entity.TRN_DATE = obj.TRN_DATE;
                        entity.TRN_NOTE = sb_master.TRN_NOTE;
                        //entity.FROM_SUB_SECTION_ID = obj.FROM_SUB_SECTION_ID;
                        //entity.TO_SUB_SECTION_ID = obj.TO_SUB_SECTION_ID;
                        //entity.FROM_USER_ID = obj.FROM_USER_ID;
                        entity.TO_USER_ID = sb_master.TO_USER_ID;
                        entity.TRN_DOCUMENT = sb_master.TRN_DOCUMENT;
                        //entity.CONTACT_ID = sb_master.CONTACT_ID;
                        entity.CONTACT_BILL_TO_ID = sb_master.CONTACT_BILL_TO_ID;
                        entity.DELIVERY_ADDRESS_ID = sb_master.DELIVERY_ADDRESS_ID;
                        entity.CUSTOMER_REF_NO = sb_master.CUSTOMER_REF_NO;
                        entity.CUSTOMER_DOCUMENT = sb_master.CUSTOMER_DOCUMENT;
                        entity.SHIPPING_MODE_ID = sb_master.SHIPPING_MODE_ID;
                        entity.SHIPPING_TYPE_ID = sb_master.SHIPPING_TYPE_ID;
                        entity.ALLOW_PARTIAL_DELIVERY = sb_master.ALLOW_PARTIAL_DELIVERY;
                        entity.REQUIRED_SAMPLE = obj.SB_CHILD_VM.Where(x => x.IS_SAMPLE).Any();
                        entity.CONTACT_NOTE = sb_master.CONTACT_NOTE;
                        entity.LAST_SDD_DATE = sb_master.LAST_SDD_DATE;
                        entity.LAST_MFG_DATE = sb_master.LAST_MFG_DATE;
                        entity.LAST_DELIVERY_DATE = sb_master.LAST_DELIVERY_DATE;
                        entity.SDD = sb_master.SDD;
                        entity.FGD = sb_master.FGD;
                        entity.LDD = sb_master.LDD;
                        entity.TRN_VALID_DAYS = sb_master.TRN_VALID_DAYS;
                        entity.TRN_AMOUNT = sb_master.TRN_AMOUNT;
                        entity.CHARGE_AMOUNT = sb_master.CHARGE_AMOUNT;
                        entity.DISCOUNT_AMOUNT = sb_master.DISCOUNT_AMOUNT;
                        entity.ADD_DISCOUNT_AMOUNT = sb_master.ADD_DISCOUNT_AMOUNT;
                        entity.VAT_AMOUNT = sb_master.VAT_AMOUNT;
                        entity.NET_AMOUNT = sb_master.NET_AMOUNT;
                        entity.ADVANCED_PAYMENT_AMOUNT = sb_master.ADVANCED_PAYMENT_AMOUNT;
                        entity.PAID_AMOUNT = sb_master.PAID_AMOUNT;
                        entity.DUE_AMOUNT = sb_master.DUE_AMOUNT;
                        entity.PAYMENT_MODE = sb_master.PAYMENT_MODE;
                        entity.PAYMENT_METHOD = sb_master.PAYMENT_METHOD;
                        entity.IS_PAID = sb_master.IS_PAID;

                        bool IsPosted = TrnAutoStepService != null && TrnAutoStepService.IS_POSTED ? TrnAutoStepService.IS_POSTED : obj.SB_MASTER_VM.IS_POSTED;
                        entity.IS_POSTED = IsPosted;
                        entity.POSTED_USER_ID = IsPosted ? userId : null;
                        entity.POSTED_DATE = IsPosted ? dateTime : null;
                        entity.POSTED_NOTE = TrnAutoStepService != null && TrnAutoStepService.IS_POSTED ? "AUTO" : (IsPosted ? "Posted" : "");

                        bool IsApprove = TrnAutoStepService != null && TrnAutoStepService.IS_APPROVE ? TrnAutoStepService.IS_APPROVE : obj.SB_MASTER_VM.IS_APPROVE;
                        IsApprove = !IsPosted ? false : IsApprove;
                        entity.IS_APPROVE = IsApprove;
                        entity.APPROVE_USER_ID = IsApprove ? userId : null;
                        entity.APPROVE_DATE = IsApprove ? dateTime : null;
                        entity.APPROVE_NOTE = TrnAutoStepService != null && TrnAutoStepService.IS_POSTED ? "AUTO" : (IsApprove ? "Approve" : "");

                        //Start Audit
                        entity.IS_ACTIVE = sb_master.IS_ACTIVE;
                        entity.UPDATE_USER = userId;
                        entity.UPDATE_DATE = dateTime;
                        entity.REVISE_NO = entity.REVISE_NO + 1;
                        //End Audit
                        dbCtx.Entry(entity).State = EntityState.Modified;

                        var entityList = dbCtx.SB_CHILD.Where(x => x.MASTER_ID == obj.SB_MASTER_VM.ID);
                        dbCtx.SB_CHILD.RemoveRange(entityList);
                        int i = 0;
                        foreach (var item in obj.SB_CHILD_VM)
                        {
                            i++;
                            SB_CHILD sb_child = new SB_CHILD();
                            ObjectMappingHelper.MapProperties<SB_CHILD_VM, SB_CHILD>(item, sb_child);

                            //new entity
                            sb_child.ID = Guid.NewGuid().ToString();
                            sb_child.MASTER_ID = sb_master.ID;
                            sb_child.LINE_NO = i;
                            sb_child.DELIVERY_DATE = item.DELIVERY_DATE ?? obj.SB_MASTER_VM.LAST_DELIVERY_DATE;
                            sb_child.DELIVERY_ADDRESS_ID = item.DELIVERY_ADDRESS_ID ?? obj.SB_MASTER_VM.DELIVERY_ADDRESS_ID;
                            //Start Audit
                            //item.IS_ACTIVE = true;
                            sb_child.CREATE_USER = userId;
                            sb_child.CREATE_DATE = dateTime;
                            sb_child.UPDATE_USER = userId;
                            sb_child.UPDATE_DATE = dateTime;
                            //item.REVISE_NO = 0;
                            //End Audit
                            sbcList.Add(sb_child);
                        }
                        dbCtx.SB_CHILD.AddRange(sbcList);

                        eQResult.rows = dbCtx.SaveChanges();
                        eQResult.success = true;
                        eQResult.messages = NotifyService.EditSuccess();
                    }
                    trn.Commit();
                    return eQResult;
                }
                catch (Exception ex)
                {
                    trn.Rollback();

                    if (ex.Message == "An error occurred while saving the entity changes. See the inner exception for details.")
                    {
                        eQResult.messages = ex.InnerException.Message;
                    }
                    else
                    {
                        eQResult.messages = ex.Message == string.Empty ? ex.InnerException.Message : ex.Message;
                    }
                    return eQResult;
                }
                finally
                {
                    dbCtx.Dispose();
                }
            }
        }


        public NEW_SB_VM GetById(string id)
        {
            var obj = new NEW_SB_VM();
            obj.SB_MASTER_VM = new SB_MASTER_VM();
            obj.SB_CHILD_VM = new List<SB_CHILD_VM>();
            List<object> param = new List<object>();
            param.Add(new SqlParameter("@MASTER_ID", id));
            string sql = @"SELECT SM.*,C.CONTACT_NAME,BT.CONTACT_NAME CONTACT_BILL_TO_NAME, CA.OFFICE_ADDRESS DELIVERY_ADDRESS ,E.EMP_NAME TO_USER_NAME
FROM SB_MASTER SM
JOIN CONTACTS C ON SM.CONTACT_ID = C.ID
JOIN CONTACTS BT ON SM.CONTACT_BILL_TO_ID = BT.ID
JOIN CONTACT_ADDRESS CA ON SM.DELIVERY_ADDRESS_ID = CA.ID
LEFT JOIN EMPLOYEES E ON SM.TO_USER_ID = E.ID
            WHERE SM.ID = @MASTER_ID";
            obj.SB_MASTER_VM = dbCtx.Database.SqlQueryRaw<SB_MASTER_VM>(sql, param.ToArray()).FirstOrDefault();

            sql = @"SELECT SC.*,P.PRODUCT_NAME,UC.UNIT_NAME,CA.OFFICE_ADDRESS DELIVERY_ADDRESS
            FROM SB_CHILD SC
            JOIN PRODUCTS P ON SC.PRODUCT_ID = P.ID
            JOIN UNIT_CHILD UC ON SC.UNIT_ID = UC.ID
            LEFT JOIN CONTACT_ADDRESS CA ON SC.DELIVERY_ADDRESS_ID = CA.ID
            WHERE SC.MASTER_ID = @MASTER_ID ORDER BY SC.LINE_NO";
            obj.SB_CHILD_VM = dbCtx.Database.SqlQueryRaw<SB_CHILD_VM>(sql, param.ToArray()).ToList();
            return obj;
        }
        public List<SB_MASTER_VM> GetAllUnposted()
        {
            string sql = @"SELECT SM.*,C.CONTACT_NAME,BT.CONTACT_NAME CONTACT_BILL_TO_NAME, CA.OFFICE_ADDRESS DELIVERY_ADDRESS ,E.EMP_NAME TO_USER_NAME
FROM SB_MASTER SM
JOIN CONTACTS C ON SM.CONTACT_ID = C.ID
JOIN CONTACTS BT ON SM.CONTACT_BILL_TO_ID = BT.ID
JOIN CONTACT_ADDRESS CA ON SM.DELIVERY_ADDRESS_ID = CA.ID
LEFT JOIN EMPLOYEES E ON SM.TO_USER_ID = E.ID ORDER BY SM.TRN_NO DESC";
            return dbCtx.Database.SqlQueryRaw<SB_MASTER_VM>(sql).ToList();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SB_MASTER";
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
                var entity = dbCtx.SB_MASTER.FirstOrDefault(x => x.IS_APPROVE && x.ID == id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.SB_MASTER.Remove(entity);

                    //child delete
                    dbCtx.SB_CHILD.RemoveRange(dbCtx.SB_CHILD.Where(x => x.MASTER_ID == id));

                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.TRN_NO!);
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
