using BS.DMO.Models.HelpDesk;
using BS.DMO.ViewModels.Transport;

namespace BS.Infra.Services.Transport
{
    public class DeliveryAgentService
    {
        private readonly AppDbContext dbCtx;
        public DeliveryAgentService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(DELIVERY_AGENT obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "DELIVERY_AGENT";
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

                    dbCtx.DELIVERY_AGENT.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.DELIVERY_AGENT.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.DELIVERY_AGENT_TYPE_ID = obj.DELIVERY_AGENT_TYPE_ID;
                            entity.AGENT_NO = obj.AGENT_NO;
                            entity.AGENT_NAME = obj.AGENT_NAME;
                            entity.AGENT_CONTACT = obj.AGENT_CONTACT;
                            entity.AGENT_ADDRESS = obj.AGENT_ADDRESS;
                            entity.LOAD_CAPACITY = obj.LOAD_CAPACITY;
                            entity.KPL = obj.KPL;
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

        public List<DELIVERY_AGENT_VM> GetAll()
        {
            string sql = $@"SELECT DA.*, EVT.VALUE_NAME DELIVERY_AGENT_TYPE
                    FROM DELIVERY_AGENT DA
                    JOIN  ENTITY_VALUE_TEXT EVT ON DA.DELIVERY_AGENT_TYPE_ID = EVT.VALUE_ID
                    ORDER BY DA.AGENT_NO";


            List<object> param = new List<object>();
            //param.Add(new SqlParameter(parameterName: "BOARD_ID", boardID));
            return dbCtx.Database.SqlQueryRaw<DELIVERY_AGENT_VM>(sql, param.ToArray()).ToList();
        }
        public List<DELIVERY_AGENT> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM DELIVERY_AGENT BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.AGENT_NO";
            return dbCtx.Database.SqlQuery<DELIVERY_AGENT>(sql).ToList();
        }
        public DELIVERY_AGENT GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM DELIVERY_AGENT BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<DELIVERY_AGENT>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "DELIVERY_AGENT";
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
                var entity = dbCtx.DELIVERY_AGENT.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.DELIVERY_AGENT.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.AGENT_NO!);
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
