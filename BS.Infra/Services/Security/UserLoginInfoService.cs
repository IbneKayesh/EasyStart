using BS.DMO.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
