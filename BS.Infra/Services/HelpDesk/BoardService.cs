namespace BS.Infra.Services.HelpDesk
{
    public class BoardService
    {
        private readonly AppDbContext dbCtx;
        public BoardService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public EQResult Insert(BOARDS obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "BOARDS";
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

                    dbCtx.BOARDS.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.BOARDS.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.PROJECT_ID = obj.PROJECT_ID;
                            entity.BOARD_NAME = obj.BOARD_NAME;
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

        public List<BOARDS> GetAll()
        {
            FormattableString sql = $@"SELECT BI.*
                    FROM BOARDS BI
                    ORDER BY BI.BOARD_NAME";
            return dbCtx.Database.SqlQuery<BOARDS>(sql).ToList();
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



        // Board Group
        public EQResult InsertBoardGroup(BOARD_GROUP obj, string userId)
        {
            DateTime dateTime = DateTime.Now;
            EQResult eQResult = new EQResult();
            eQResult.entities = "BOARDS";
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

                    dbCtx.BOARD_GROUP.Add(obj);
                    eQResult.rows = dbCtx.SaveChanges();
                    eQResult.success = true;
                    eQResult.messages = NotifyService.SaveSuccess();
                    return eQResult;
                }
                else
                {
                    //old entity
                    var entity = dbCtx.BOARD_GROUP.Find(obj.ID);
                    if (entity != null)
                    {
                        if (entity.RowVersion.SequenceEqual(obj.RowVersion))
                        {
                            //TODO : Update property
                            entity.BOARD_ID = obj.BOARD_ID;
                            entity.GROUP_NAME = obj.GROUP_NAME;
                            entity.BS_COLOR = obj.BS_COLOR;
                            entity.ORDER_BY = obj.ORDER_BY;
                            entity.LIMIT_ROWS = obj.LIMIT_ROWS;
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

        public List<BOARD_GROUP> GetBoardGroupWithChildByBoardID(string boardID)
        {
            List<object> param = new List<object>();
            if (!string.IsNullOrWhiteSpace(boardID))
            {
                param.Add(new SqlParameter(parameterName: "BOARD_ID", boardID));
                string sql = "select * from BOARD_GROUP bg where bg.BOARD_ID = @BOARD_ID order by bg.ORDER_BY";
                var entity = dbCtx.Database.SqlQueryRaw<BOARD_GROUP>(sql, param.ToArray()).ToList();
                foreach (BOARD_GROUP item in entity)
                {
                    param = new List<object>();
                    param.Add(new SqlParameter(parameterName: "BG_ID", item.ID));
                    sql = $@"SELECT * FROM
                        (
                        SELECT TOP {item.LIMIT_ROWS} WT.*,TS.STATUS_NAME,TS.BS_COLOR FROM WORK_TASK WT
                        JOIN TASK_STATUS TS on WT.STATUS_ID = TS.ID
                        WHERE WT.BG_ID = @BG_ID
                        ) W
                        ORDER BY W.REQUEST_DATE";
                    var wt_entity = dbCtx.Database.SqlQueryRaw<WORK_TASK_VM>(sql, param.ToArray()).ToList();
                    foreach(WORK_TASK_VM wt  in wt_entity)
                    {
                        wt.WAIT_DURATION = CalculateTotalDuration(wt.REQUEST_DATE, wt.WORK_START_DATE);
                        wt.TOTAL_DURATION = CalculateTotalDuration(wt.REQUEST_DATE, wt.WORK_END_DATE);
                    }
                    item.WORK_TASK_VM = wt_entity;
                }
                return entity;
            }
            else
            {
                return new List<BOARD_GROUP>();
            }
        }
        static string CalculateTotalDuration(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null && endDate != null)
            {
                try
                {
                    TimeSpan duration = (endDate - startDate).Value;
                    return $"{duration.Days}d {duration.Hours}h {duration.Minutes}m";
                }
                catch (FormatException ex)
                {
                    // Log or print the exception details
                    Console.WriteLine($"Error formatting date: {ex.Message}");
                }
            }
            else if (startDate != null)
            {
                return startDate.ToString();
            }

            return "-";
        }
        public List<BOARD_GROUP> GetBoardGroupByBoardID(string boardID)
        {
            List<object> param = new List<object>();
            if (!string.IsNullOrWhiteSpace(boardID))
            {
                param.Add(new SqlParameter(parameterName: "BOARD_ID", boardID));
                string sql = "select * from BOARD_GROUP bg where bg.BOARD_ID = @BOARD_ID order by bg.ORDER_BY";
                var entity = dbCtx.Database.SqlQueryRaw<BOARD_GROUP>(sql, param.ToArray()).ToList();
                return entity;
            }
            else
            {
                return new List<BOARD_GROUP>();
            }
        }
        //public List<BOARDS> GetAllActive()
        //{
        //    FormattableString sql = $@"SELECT BI.*
        //            FROM BOARDS BI
        //            WHERE BI.IS_ACTIVE = 1
        //            ORDER BY BI.BOARD_NAME";
        //    return dbCtx.Database.SqlQuery<BOARDS>(sql).ToList();
        //}
        public BOARD_GROUP GetBoardGroupByID(string id, string boardID)
        {

            var obj = new BOARD_GROUP();
            if (!string.IsNullOrWhiteSpace(id))
            {
                obj.ID = id;
            }
            obj.BOARD_ID = boardID;
            var entity = dbCtx.BOARD_GROUP.Find(obj.ID);
            if (entity != null)
            {
                obj = entity;
                //obj.GROUP_NAME = entity.GROUP_NAME;
                //obj.BS_COLOR = entity.BS_COLOR;
                //obj.ORDER_BY = entity.ORDER_BY;
                //obj.LIMIT_ROWS = entity.LIMIT_ROWS;
            }

            return obj;
        }

        //public EQResult Delete(string id)
        //{
        //    EQResult eQResult = new EQResult();
        //    eQResult.entities = "BOARDS";
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        eQResult.messages = NotifyService.InvalidRequestString();
        //        return eQResult;
        //    }
        //    try
        //    {
        //        //check child entity
        //        //int anyChild = dbCtx.BANK_BRANCH.Where(x => x.BANK_ID == id).Count();
        //        //if (anyChild > 0)
        //        //{
        //        //    eQResult.messages = NotifyService.DeleteHasChildString("Branch", anyChild, "Bank");
        //        //    return eQResult;
        //        //}

        //        //old entity
        //        var entity = dbCtx.BOARDS.Find(id);
        //        if (entity != null)
        //        {
        //            //TODO : Delete property
        //            dbCtx.BOARDS.Remove(entity);
        //            eQResult.rows = dbCtx.SaveChanges();
        //            eQResult.success = true;
        //            eQResult.messages = NotifyService.DeletedSuccessString(entity.BOARD_NAME!);
        //            return eQResult;
        //        }
        //        else
        //        {
        //            eQResult.messages = NotifyService.NotFoundString();
        //            return eQResult;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message.Contains("See the inner exception for details")
        //                      ? ex.InnerException?.Message ?? ex.Message
        //                      : ex.Message;
        //        error = error.Replace("'", "");
        //        eQResult.messages = error;
        //        return eQResult;
        //    }
        //    finally
        //    {
        //        dbCtx.Dispose();
        //    }
        //}
    }
}