using BS.DMO.Models.Security;
using BS.DMO.Models.Setup.Security;

namespace BS.Infra.Services.Security
{
    public class SecurityRoleService
    {
        private readonly AppDbContext dbCtx;
        public SecurityRoleService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(SECURITY_ROLE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SECURITY_ROLE";
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

                    dbCtx.SECURITY_ROLE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.SECURITY_ROLE.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            //entity.BANK_NAME = obj.BANK_NAME;
                            //entity.SHORT_NAME = obj.SHORT_NAME;
                            //entity.CO_ADDRESS = obj.CO_ADDRESS;
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

        public List<SECURITY_ROLE> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SECURITY_ROLE BI
                    ORDER BY BI.ROLE_NAME";
            return dbCtx.Database.SqlQuery<SECURITY_ROLE>(sql).ToList();
        }
        public List<SECURITY_ROLE> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SECURITY_ROLE BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.ROLE_NAME";
            return dbCtx.Database.SqlQuery<SECURITY_ROLE>(sql).ToList();
        }
        public List<SECURITY_ROLE> GetAllActiveWithoutMenuId(string menuId)
        {
            FormattableString sql = $@"SELECT SR.*
                    FROM SECURITY_ROLE SR
					LEFT JOIN MENU_ROLE MR ON SR.ID = MR.ROLE_ID AND MR.MENU_ID = {menuId}
                    WHERE SR.IS_ACTIVE = 1 AND MR.ROLE_ID IS NULL
                    ORDER BY SR.ROLE_NAME";
            return dbCtx.Database.SqlQuery<SECURITY_ROLE>(sql).ToList();
        }
        public SECURITY_ROLE GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SECURITY_ROLE BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<SECURITY_ROLE>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SECURITY_ROLE";
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
                var entity = dbCtx.SECURITY_ROLE.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.SECURITY_ROLE.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.ROLE_NAME!);
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
