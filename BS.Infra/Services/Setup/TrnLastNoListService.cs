using System.Security.Cryptography.X509Certificates;
using System;
using BS.DMO.StaticValues;

namespace BS.Infra.Services.Setup
{
    public class TrnLastNoListService
    {
        private readonly AppDbContext dbCtx;
        public TrnLastNoListService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public static string CreateTransactionNo(string transactionId)
        {
            return transactionId +
               DateTime.Now.ToString("yyMMdd") +
              "xx-xxxxx";
        }

        public string CreateTransactionNo(AppDbContext dbCtx, string transactionId, string subSectionId, DateTime dateTime)
        {
            int last_no = 1;
            string shortName = "";
            var query = dbCtx.TRN_LAST_NO_LIST.Where(x => x.TRANSACTION_ID == transactionId.ToString() && x.SUB_SECTION_ID == subSectionId && x.YEAR_ID == dateTime.Year && x.MONTH_ID == dateTime.Month).FirstOrDefault();
            if (query != null)
            {
                last_no = query.LAST_NO;
                shortName = query.SHORT_NAME;
            }
            else
            {
                var sectionEntity = dbCtx.SUB_SECTIONS.AsNoTracking().Where(x => x.IS_ACTIVE == true && x.ID == subSectionId).FirstOrDefault();
                if (sectionEntity == null)
                {
                    //inactive or empty or not allowed
                    return string.Empty;
                }
                shortName = sectionEntity.SHORT_NAME;

                TRN_LAST_NO_LIST obj = new TRN_LAST_NO_LIST();
                obj.ID = Guid.NewGuid().ToString();
                obj.TRANSACTION_ID = transactionId.ToString();
                obj.SUB_SECTION_ID = subSectionId;
                obj.SHORT_NAME = sectionEntity.SHORT_NAME;
                obj.YEAR_ID = dateTime.Year;
                obj.MONTH_ID = dateTime.Month;
                obj.LAST_NO = last_no;
                dbCtx.TRN_LAST_NO_LIST.Add(obj);
                dbCtx.SaveChanges();

                // Detach the entity after saving
                dbCtx.Entry(obj).State = EntityState.Detached;
            }
            // Update trn with same trn
            UpdateTransactionNo(dbCtx, TransactionID.SB, subSectionId, dateTime);

            return transactionId.ToString() +
                dateTime.ToString("yyMMdd") +
                shortName +
                "-" +
                last_no.ToString().PadLeft(5, '0');
        }
        public bool UpdateTransactionNo(AppDbContext dbCtx, string transactionId, string subSectionId, DateTime dateTime)
        {
            int last_no = 1;
            var query = dbCtx.TRN_LAST_NO_LIST.FirstOrDefault(x => x.TRANSACTION_ID == transactionId.ToString() && x.SUB_SECTION_ID == subSectionId && x.YEAR_ID == dateTime.Year && x.MONTH_ID == dateTime.Month);
            if (query != null)
            {
                last_no = query.LAST_NO + 1;
                query.LAST_NO = last_no;
                dbCtx.Entry(query).State = EntityState.Modified;
                dbCtx.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
