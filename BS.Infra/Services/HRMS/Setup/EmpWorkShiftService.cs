namespace BS.Infra.Services.HRMS.Setup
{
    public class EmpWorkShiftService
    {
        private readonly AppDbContext dbCtx;
        public EmpWorkShiftService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }

        public EQResult Insert(EMP_WORK_SHIFT obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_WORK_SHIFT";
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

                    dbCtx.EMP_WORK_SHIFT.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.EMP_WORK_SHIFT.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            //entity.EMP_ID = obj.EMP_ID;
                            //entity.WORK_SHIFT_ID = obj.WORK_SHIFT_ID;
                            entity.FROM_DATE = obj.FROM_DATE;
                            entity.TO_DATE = obj.TO_DATE;
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

        public EMP_WORK_SHIFT_VM_IDX GetByEmpID(string empId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@EMP_ID", empId));
            var obj = new EMP_WORK_SHIFT_VM_IDX();
            obj.EMP_ID = empId;
            string sql = $@"SELECT EWS.*,WS.SHIFT_NAME,WS.IN_TIME_START,WS.IN_TIME_END,WS.OUT_TIME_START,WS.OUT_TIME_END,WS.GRACE_MINUTE,WS.MAX_OT_HOUR
FROM EMP_WORK_SHIFT EWS
JOIN WORK_SHIFT WS ON EWS.WORK_SHIFT_ID = WS.ID
WHERE EWS.EMP_ID = @EMP_ID ORDER BY EWS.FROM_DATE";
            obj.EMP_WORK_SHIFT_VM = dbCtx.Database.SqlQueryRaw<EMP_WORK_SHIFT_VM>(sql, param.ToArray()).ToList();
            return obj;
        }

        public EMP_WORK_SHIFT GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.* FROM EMP_WORK_SHIFT BI WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<EMP_WORK_SHIFT>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "EMP_WORK_SHIFT";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //old entity
                var entity = dbCtx.EMP_WORK_SHIFT.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.EMP_WORK_SHIFT.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.FROM_DATE + " ~ " + entity.TO_DATE);
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
