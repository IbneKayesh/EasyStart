namespace BS.Infra.Services.Company
{
    public class SubSectionService
    {
        private readonly AppDbContext dbCtx;
        public SubSectionService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(SUB_SECTIONS obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SUB_SECTIONS";
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

                    dbCtx.SUB_SECTIONS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.SUB_SECTIONS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.SECTION_ID = obj.SECTION_ID;
                            entity.SUB_SECTION_NAME = obj.SUB_SECTION_NAME;
                            entity.SHORT_NAME = obj.SHORT_NAME;
                            entity.ADDRESS_INFO = obj.ADDRESS_INFO;
                            entity.CONTACT_NAME = obj.CONTACT_NAME;
                            entity.CONTACT_NO = obj.CONTACT_NO;
                            entity.EMAIL_ADDRESS = obj.EMAIL_ADDRESS;
                            entity.MAX_EMPLOYEE = obj.MAX_EMPLOYEE;
                            entity.MAX_SALARY = obj.MAX_SALARY;
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

        public List<SUB_SECTIONS_VM> GetAll()
        {
            FormattableString sql = $@"SELECT S.*,D.SECTION_NAME
FROM SUB_SECTIONS S
JOIN SECTIONS D ON S.SECTION_ID = D.ID
ORDER BY D.ID, S.SUB_SECTION_NAME";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS_VM>(sql).ToList();
        }
        public List<SUB_SECTIONS> GetAllActive()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SUB_SECTIONS BI
                    WHERE BI.IS_ACTIVE = 1
                    ORDER BY BI.SUB_SECTION_NAME";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS>(sql).ToList();
        }

        public List<SUB_SECTIONS> GetAllByTrnID(string trnId)
        {
            FormattableString sql = $@"SELECT SS.*
                        FROM SUB_SECTIONS SS
                        JOIN SUB_SECTIONS_TRN_ID SST ON SS.ID = SST.SUB_SECTION_ID
                        WHERE SS.IS_ACTIVE = 1
                        AND SST.IS_ACTIVE = 1
                        AND SST.TRN_ID = {trnId}
                        ORDER BY SS.SUB_SECTION_NAME";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS>(sql).ToList();
        }
        public SUB_SECTIONS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SUB_SECTIONS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SUB_SECTIONS";
            if (string.IsNullOrWhiteSpace(id))
            {
                eQResult.messages = NotifyService.InvalidRequestString();
                return eQResult;
            }
            try
            {
                ////check child entity
                //int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
                //if (anyChild > 0)
                //{
                //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
                //    return eQResult;
                //}

                //old entity
                var entity = dbCtx.SUB_SECTIONS.Find(id);
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.SUB_SECTIONS.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(entity.SUB_SECTION_NAME!);
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
