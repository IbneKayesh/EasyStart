namespace BS.Infra.Services.Setup
{
    public class LeaveTypeService
    {
        private readonly AppDbContext dbCtx;
        public LeaveTypeService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(LEAVE_TYPE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "LEAVE_TYPE";
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

                    dbCtx.LEAVE_TYPE.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.LEAVE_TYPE.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.LEAVE_TYPE_NAME = obj.LEAVE_TYPE_NAME;
                            entity.IS_WORKING_DAY = obj.IS_WORKING_DAY;
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

        public List<LEAVE_TYPE> GetAll()
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM LEAVE_TYPE LT
                    ORDER BY LT.LEAVE_TYPE_NAME";
            return dbCtx.Database.SqlQuery<LEAVE_TYPE>(sql).ToList();
        }
        public List<LEAVE_TYPE> GetAllActive()
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM LEAVE_TYPE LT
                    WHERE LT.IS_ACTIVE = 1
                    ORDER BY LT.LEAVE_TYPE_NAME";
            return dbCtx.Database.SqlQuery<LEAVE_TYPE>(sql).ToList();
        }
        public LEAVE_TYPE GetById(string id)
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM LEAVE_TYPE LT
                    WHERE LT.ID = {id}";
            return dbCtx.Database.SqlQuery<LEAVE_TYPE>(sql).ToList().FirstOrDefault()!;
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "LEAVE_TYPE";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //check child entity
                //int anyChild = dbCtx.LEAVE_TYPE.Where(x => x.ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.LEAVE_TYPE.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.LEAVE_TYPE.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.LEAVE_TYPE_NAME!);
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
