using BS.DMO.ViewModels.CRM;

namespace BS.Infra.Services.CRM
{
    public class ContactsService
    {
        private readonly AppDbContext dbCtx;
        public ContactsService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(CONTACTS obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "CONTACTS";
            try
            {
                if(!obj.IS_CUSTOMER && !obj.IS_SELLER)
                {
                    eQResult.messages = NotifyService.Error("Contact must be a Customer or Seller");
                    return eQResult;
                }
                if (obj.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.ID = Guid.NewGuid().ToString();

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = dateTime;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = dateTime;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.CONTACTS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.CONTACTS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.CONTACT_CODE = obj.CONTACT_CODE;
                            entity.CONTACT_CATEGORY_ID = obj.CONTACT_CATEGORY_ID;
                            entity.CONTACT_GROUP = obj.CONTACT_GROUP;
                            entity.CONTACT_NAME = obj.CONTACT_NAME;
                            entity.CONTACT_PERSON = obj.CONTACT_PERSON;
                            entity.CONTACT_NO = obj.CONTACT_NO;
                            entity.EMAIL_ADDRESS = obj.EMAIL_ADDRESS;
                            entity.OFFICE_ADDRESS = obj.OFFICE_ADDRESS;
                            entity.FACTORY_ADDRESS = obj.FACTORY_ADDRESS;

                            entity.IS_CUSTOMER = obj.IS_CUSTOMER;
                            entity.CUSTOMER_USER_ID = userId;
                            entity.CUSTOMER_DATE = dateTime;

                            entity.IS_SELLER = obj.IS_SELLER;
                            entity.SELLER_USER_ID = userId;
                            entity.SELLER_DATE = dateTime;

                            //Start Audit
                            entity.IS_ACTIVE = obj.IS_ACTIVE;
                            entity.UPDATE_USER = userId;
                            entity.UPDATE_DATE = dateTime;
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

        public List<CONTACTS_VM> GetAll()
        {
            string sql = $@"SELECT C.*,EVT.VALUE_NAME CATEGORY_NAME
                    FROM CONTACTS C
					JOIN ENTITY_VALUE_TEXT EVT ON C.CONTACT_CATEGORY_ID = EVT.VALUE_ID
                    ORDER BY C.CONTACT_CATEGORY_ID,C.CONTACT_GROUP,C.CONTACT_NAME";
            return dbCtx.Database.SqlQueryRaw<CONTACTS_VM>(sql).ToList();
        }
        public List<CONTACTS> GetByName(string contactName)
        {
           return dbCtx.CONTACTS.Where(x=>x.IS_CUSTOMER && x.CONTACT_NAME.Contains(contactName)).ToList();
        }
        public List<CONTACTS> GetAllCustomer()
        {
           return dbCtx.CONTACTS.Where(x=>x.IS_CUSTOMER).ToList();
        }  
        
        public List<CONTACTS> GetAllCustomerGroupById(string contactId)
        {
            string sql = @"select C.* from CONTACTS C
Join CONTACTS CB on C.CONTACT_GROUP = CB.CONTACT_GROUP
Where C.IS_CUSTOMER = 1 AND CB.ID = @ID";

            List<object> param = new List<object>();
            param.Add(new SqlParameter("@ID", contactId));
            return dbCtx.Database.SqlQueryRaw<CONTACTS>(sql, param.ToArray()).ToList();
        }
        public List<CONTACTS> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM CONTACTS BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.CONTACT_NAME";
            return dbCtx.Database.SqlQuery<CONTACTS>(sql).ToList();
        }
        public CONTACTS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM CONTACTS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<CONTACTS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "CONTACTS";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                if (anyChild > 0)
                {
                    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.CONTACTS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.CONTACTS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.CONTACT_NAME!);
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

        //Contact Address
        public EQResult Insert_ContactAddress(CONTACT_ADDRESS obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "CONTACT_ADDRESS";
            try
            {
                if (obj.ID == Guid.Empty.ToString())
                {
                    //new entity
                    obj.ID = Guid.NewGuid().ToString();

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = dateTime;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = dateTime;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.CONTACT_ADDRESS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.CONTACT_ADDRESS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.CONTACT_PERSON = obj.CONTACT_PERSON;
                            entity.CONTACT_NO = obj.CONTACT_NO;
                            entity.EMAIL_ADDRESS = obj.EMAIL_ADDRESS;
                            entity.OFFICE_ADDRESS = obj.OFFICE_ADDRESS;
                            entity.IS_DEFAULT = obj.IS_DEFAULT;
                            //Start Audit
                            entity.IS_ACTIVE = obj.IS_ACTIVE;
                            entity.UPDATE_USER = userId;
                            entity.UPDATE_DATE = dateTime;
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
                if(ex.Message.Contains("See the inner exception for details"))
                {
                    eQResult.messages = NotifyService.Error(ex.InnerException.Message);
                }
                else
                {
                    eQResult.messages = NotifyService.Error(ex.Message == string.Empty ? ex.InnerException.Message : ex.Message);
                }
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }

        public List<CONTACT_ADDRESS> GetAll_ContactAddress(string contactId)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM CONTACT_ADDRESS BI WHERE BI.CONTACT_ID = {contactId}
                    ORDER BY BI.CONTACT_PERSON";
            return dbCtx.Database.SqlQuery<CONTACT_ADDRESS>(sql).ToList();
        }
        public CONTACT_ADDRESS GetById_ContactAddress(string id)
        {
            string sql = $@"SELECT BI.*
                    FROM CONTACT_ADDRESS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQueryRaw<CONTACT_ADDRESS>(sql).ToList().FirstOrDefault();
        }

        public CONTACT_ADDRESS GetById_ContactDefaultAddress(string id)
        {
            string sql = $@"SELECT CA.* FROM CONTACT_ADDRESS CA WHERE CA.CONTACT_ID ='{id}' AND CA.IS_DEFAULT = 1";
            return dbCtx.Database.SqlQueryRaw<CONTACT_ADDRESS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete_ContactAddress(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "CONTACT_ADDRESS";
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
                var entity = dbCtx.CONTACT_ADDRESS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.CONTACT_ADDRESS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.OFFICE_ADDRESS!);
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
