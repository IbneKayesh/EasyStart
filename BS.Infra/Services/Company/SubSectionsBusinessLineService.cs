namespace BS.Infra.Services.Company
{
    public class SubSectionsBusinessLineService
    {
        private readonly AppDbContext dbCtx;
        public SubSectionsBusinessLineService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(SUB_SECTIONS_BUSINESS_LINE obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SUB_SECTIONS_BUSINESS_LINE";
            try
            {
                //old entity
                var entity = dbCtx.SUB_SECTIONS_BUSINESS_LINE.Where(x => x.SUB_SECTION_ID == obj.SUB_SECTION_ID && x.BUSINESS_LINE_ID == obj.BUSINESS_LINE_ID).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                    {
                        //TODO : Update property
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
                    obj.IS_ACTIVE = true;
                    obj.CREATE_USER = userId;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.UPDATE_USER = userId;
                    obj.UPDATE_DATE = DateTime.Now;
                    //obj.REVISE_NO = 0;
                    //End Audit

                    dbCtx.SUB_SECTIONS_BUSINESS_LINE.Add(obj);
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

        public List<SUB_SECTIONS_BUSINESS_LINE_VM> GetAllBySubSectionID(string subsectionId)
        {
            FormattableString sql = $@"SELECT SSBL.*,BL.BUSINESS_LINE_NAME
FROM SUB_SECTIONS_BUSINESS_LINE SSBL
JOIN BUSINESS_LINE BL ON SSBL.BUSINESS_LINE_ID = BL.ID
WHERE SSBL.SUB_SECTION_ID = {subsectionId}
ORDER BY BL.BUSINESS_LINE_NAME";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS_BUSINESS_LINE_VM>(sql).ToList();
        }
        public SUB_SECTIONS GetById(string id)
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM SUB_SECTIONS BI
                    WHERE BI.ID = {id}";
            return dbCtx.Database.SqlQuery<SUB_SECTIONS>(sql).ToList().FirstOrDefault();
        }

        public EQResult Delete(string id, string business_line_id)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "SUB_SECTIONS_BUSINESS_LINE";
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
                var entity = dbCtx.SUB_SECTIONS_BUSINESS_LINE.Where(x => x.SUB_SECTION_ID == id && x.BUSINESS_LINE_ID == business_line_id).FirstOrDefault();
                if (entity != null)
                {
                    //TODO : Delete property
                    dbCtx.SUB_SECTIONS_BUSINESS_LINE.Remove(entity);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccess();
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
