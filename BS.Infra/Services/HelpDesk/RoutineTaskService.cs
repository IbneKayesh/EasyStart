namespace BS.Infra.Services.HelpDesk
{
    public class RoutineTaskService
    {
        private readonly AppDbContext dbCtx;
        public RoutineTaskService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(ROUTINE_TASK_CREATE_VM obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "ROUTINE_TASK";
            try
            {
                var oldlog = dbCtx.ROUTINE_TASK.Where(x => x.ROUTINE_DATE.Date == dateTime.Date).ToList();
                dbCtx.ROUTINE_TASK.RemoveRange(oldlog);
                dbCtx.SaveChanges();

                List<ROUTINE_TASK> objList = new List<ROUTINE_TASK>();
                foreach (var item in obj.ROUTINE_TASK_VM)
                {
                    if (item.IS_DONE=="true")
                    {
                        objList.Add(new ROUTINE_TASK
                        {
                            //new entity
                            ID = Guid.NewGuid().ToString(),
                            USER_ID = userId,
                            ROUTINE_NAMES_ID = item.ROUTINE_NAMES_ID,
                            ROUTINE_DATE = dateTime,
                            ROUTINE_NOTE = item.ROUTINE_NOTE,
                            IS_DONE = item.IS_DONE == "true" ? true : false,
                            //Start Audit
                            //obj.IS_ACTIVE = true;
                            CREATE_USER = userId,
                            CREATE_DATE = dateTime,
                            UPDATE_USER = userId,
                            UPDATE_DATE = dateTime,
                            REVISE_NO = 0,
                            //End Audit
                        });
                    }                   
                }
                dbCtx.ROUTINE_TASK.AddRange(objList);
                eQResult.rows = dbCtx.SaveChanges();
                eQResult.success = true;
                eQResult.messages = NotifyService.SaveSuccess();
                return eQResult;
            }
            catch (Exception ex)
            {
                string error = ex.Message.Contains("See the inner exception for details")
                                ? ex.InnerException?.Message ?? ex.Message
                                : ex.Message;
                error = error.Replace("'", "");
                eQResult.messages = NotifyService.Error(error);
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }

        public ROUTINE_TASK_CREATE_VM CreateNew(DateTime dateTime)
        {
            string sql = $@"select rn.ROUTINE_NAME,rn.ROUTINE_FREQUENCY,rt.ID,rn.ID ROUTINE_NAMES_ID,rt.ROUTINE_DATE,rt.ROUTINE_NOTE, CASE WHEN ISNULL(rt.IS_DONE, 0) = 1 THEN 'true' else 'false' end IS_DONE
from ROUTINE_NAMES rn
Left Join ROUTINE_TASK rt on rn.ID = rt.ROUTINE_NAMES_ID
AND CAST(ISNULL(rt.ROUTINE_DATE, GETDATE()) AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY rt.IS_DONE,rn.ROUTINE_NAME";
            var entities = dbCtx.Database.SqlQueryRaw<ROUTINE_TASK_VM>(sql).ToList();
            return new ROUTINE_TASK_CREATE_VM
            {
                ROUTINE_TASK_VM = entities
            };
        }
        public List<ROUTINE_TASK_VM> GetAll()
        {
            string sql = $@"select rn.ROUTINE_NAME,rn.ROUTINE_FREQUENCY,rt.ID,rn.ID ROUTINE_NAMES_ID,rt.ROUTINE_DATE,rt.ROUTINE_NOTE, CASE WHEN ISNULL(rt.IS_DONE, 0) = 1 THEN 'true' else 'false' end IS_DONE
from ROUTINE_NAMES rn
Join ROUTINE_TASK rt on rn.ID = rt.ROUTINE_NAMES_ID
WHERE  rt.ROUTINE_DATE BETWEEN 
      DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) 
      AND 
      EOMONTH(GETDATE())
ORDER BY rt.ROUTINE_DATE DESC, rt.IS_DONE, rn.ROUTINE_NAME";
            return dbCtx.Database.SqlQueryRaw<ROUTINE_TASK_VM>(sql).ToList();
        }
        public List<BOARDS> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BOARDS BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.BOARD_NAME";
            return dbCtx.Database.SqlQuery<BOARDS>(sql).ToList();
        }
        public BOARDS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BOARDS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<BOARDS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "BOARDS";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                int anyChild = dbCtx.WORK_TASK.Where(x => x.BG_ID == id).Count();
                if (anyChild > 0)
                {
                    eQResult.messages = NotifyService.DeleteHasChildString("Work Task", anyChild, "Board");
                    return eQResult;
                }

                //old entity
                var entity = dbCtx.BOARDS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.BOARDS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.BOARD_NAME!);
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
                string error = ex.Message.Contains("See the inner exception for details")
                              ? ex.InnerException?.Message ?? ex.Message
                              : ex.Message;
                error = error.Replace("'", "");
                eQResult.messages = error;
                return eQResult;
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
    }
}
