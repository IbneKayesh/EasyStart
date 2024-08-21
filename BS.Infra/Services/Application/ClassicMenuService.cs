
using BS.DMO.Models.Security;
using BS.DMO.ViewModels.Application;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BS.Infra.Services.Application
{
    public class ClassicMenuService
    {
        private readonly AppDbContext dbCtx;
        public ClassicMenuService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public List<CLASSIC_MENU> GetAll(string prev, string next)
        {
            if (next != null)
            {

                FormattableString sql = $@"select * from CLASSIC_MENU where PARENT_NODE={next} order by PAGE_ID";
                return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
            }
            else if (prev != null)
            {

                //FormattableString sql = $@"select * from CLASSIC_MENU where MENU_ID={prev}";
                FormattableString sql = $@"select PM.*
                                        from CLASSIC_MENU PM
                                        JOIN CLASSIC_MENU CM on PM.PARENT_NODE=CM.PARENT_NODE
                                        where CM.MENU_ID={prev} order by PM.PAGE_ID";
                return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
            }
            else
            {

                FormattableString sql = $@"select * from CLASSIC_MENU where PARENT_NODE='N' order by MENU_ID";
                return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
            }
        }

        public List<CLASSIC_MENU> GetAll()
        {
            FormattableString sql = $@"select * from CLASSIC_MENU where IS_ACTIVE = 1 order by MENU_ID";
            return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
        }
        public List<CLASSIC_MENU_VM> GetAllByRoleId(string roleId)
        {
            string sql = $@"select CM.*,MR.IS_SELECT, MR.IS_INSERT, MR.IS_UPDATE, MR.IS_DELETE
from CLASSIC_MENU CM
join MENU_ROLE MR on CM.MENU_ID = MR.MENU_ID
where MR.ROLE_ID = '{roleId}'
and MR.IS_ACTIVE = 1
           union
            select cm.*,CAST(0 AS bit)IS_SELECT,CAST(0 AS bit)IS_INSERT,CAST(0 AS bit)IS_UPDATE,CAST(0 AS bit)IS_DELETE from CLASSIC_MENU cm where CHILD_NODE = 'Y'
order by CM.MENU_ID";

            //object[] parameters = entityIds.Select((id, index) => new SqlParameter($"p{id}", id)).ToArray();
            return dbCtx.Database.SqlQueryRaw<CLASSIC_MENU_VM>(sql).ToList();
            
        }

        //Menu Role :: compostie to one
        public MENU_ROLE GetMenuRoleByMenuIdRoleId(string id, string roleId)
        {
            FormattableString sql = $@"SELECT MR.*
                    FROM MENU_ROLE MR
                    WHERE MR.MENU_ID = {id} and MR.ROLE_ID = {roleId}";
            return dbCtx.Database.SqlQuery<MENU_ROLE>(sql).ToList().FirstOrDefault();
        }
        public EQResult InsertMenuRole(MENU_ROLE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "MENU_ROLE";
            try
            {
                //old entity
                var entity = dbCtx.MENU_ROLE.Where(x => x.MENU_ID == obj.MENU_ID && x.ROLE_ID == obj.ROLE_ID).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                    {
                        //TODO : Update property
                        entity.IS_SELECT = obj.IS_SELECT;
                        entity.IS_INSERT = obj.IS_INSERT;
                        entity.IS_UPDATE = obj.IS_UPDATE;
                        entity.IS_DELETE = obj.IS_DELETE;
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
                    //new entity

                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.MENU_ROLE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
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

        public List<MENU_ROLE> GetRoleByMenuId(string id)
        {
            FormattableString sql = $@"select * from MENU_ROLE where MENU_ID ={id} order by ROLE_ID";
            return dbCtx.Database.SqlQuery<MENU_ROLE>(sql).ToList();
        }
    }
}
