using BS.DMO.Models.Security;

namespace BS.Infra.Services.Security
{
    public class UserLoginInfoService
    {
        private readonly AppDbContext dbCtx;
        public UserLoginInfoService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public void AddLog(string userId, string sessionId)
        {
            try
            {
                USER_LOGIN_INFO obj = new USER_LOGIN_INFO
                {
                    ID = Guid.NewGuid().ToString(),
                    USER_ID = userId,
                    SESSION_ID = sessionId,
                    IN_TIME = DateTime.Now
                };

                dbCtx.USER_LOGIN_INFO.Add(obj);
                dbCtx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while adding login log: {ex.Message}", ex);
                //throw new Exception();
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
        public List<USER_LOGIN_INFO> GetAll()
        {
            try
            {
                return dbCtx.USER_LOGIN_INFO.Where(x => x.IN_TIME.Date == DateTime.Now.Date).OrderByDescending(x => x.IN_TIME).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while adding login log: {ex.Message}", ex);
                //throw new Exception();
            }
            finally
            {
                dbCtx.Dispose();
            }
        }
    }
}
