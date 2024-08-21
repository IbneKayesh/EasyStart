namespace BS.Infra.Services
{
    public class BaseService
    {
        public EQResult InsertAppNotifications(AppDbContext dbCtx, APP_NOTIFICATIONS obj, string userId)
        {
            EQResult eQResult = new EQResult();
            eQResult.entities = "APP_NOTIFICATIONS";
            try
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

                dbCtx.APP_NOTIFICATIONS.Add(obj);
                eQResult.rows = dbCtx.SaveChanges();
                eQResult.success = true;
                eQResult.messages = NotifyService.SaveSuccess();

                //Notification

                return eQResult;
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

    }
}
