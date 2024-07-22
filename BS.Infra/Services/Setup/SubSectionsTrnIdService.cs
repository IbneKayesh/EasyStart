namespace BS.Infra.Services.Setup
{
    public class SubSectionsTrnIdService
    {
        private readonly AppDbContext dbCtx;
        public SubSectionsTrnIdService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(SUB_SECTIONS_TRN_ID obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SUB_SECTIONS_TRN_ID";
            try
            {
                //old entity
                var entity = dbCtx.SUB_SECTIONS_TRN_ID.Find(obj.TRN_ID);
                if (entity != null)
                {
                    if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                    {
                        //TODO : Update property
                        //entity.IS_POSTED = obj.IS_POSTED;
                        //entity.IS_APPROVE = obj.IS_APPROVE;
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
                    //Start Audit
                    //obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.SUB_SECTIONS_TRN_ID.Add(obj);
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

        public List<SUB_SECTIONS_TRN_ID> GetAll()
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM SUB_SECTIONS_TRN_ID LT
                    ORDER BY LT.TRN_ID";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS_TRN_ID>(sql).ToList();
        }
        public List<SUB_SECTIONS_TRN_ID> GetAllActive()
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM SUB_SECTIONS_TRN_ID LT
                    WHERE LT.IS_ACTIVE = 1
                    ORDER BY LT.TRN_ID";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS_TRN_ID>(sql).ToList();
        }
        public SUB_SECTIONS_TRN_ID GetById(string id)
        {
            FormattableString sql = $@"SELECT LT.*
                    FROM SUB_SECTIONS_TRN_ID LT
                    WHERE LT.TRN_ID = {id}";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS_TRN_ID>(sql).ToList().FirstOrDefault()!;
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SUB_SECTIONS_TRN_ID";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                //old entity
                var entity = dbCtx.SUB_SECTIONS_TRN_ID.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.SUB_SECTIONS_TRN_ID.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.TRN_ID!);
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
