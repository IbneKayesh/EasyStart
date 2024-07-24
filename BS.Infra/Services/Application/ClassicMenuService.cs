using BS.DMO.Models.Application;

namespace BS.Infra.Services.Application
{
    public class ClassicMenuService
    {
        private readonly AppDbContext dbCtx;
        public ClassicMenuService(AppDbContext _dbContext)
        {
            dbCtx = _dbContext;
        }
        public List<CLASSIC_MENU> GetAll(string prev, string next)
        {
            if (next != null)
            {

                FormattableString sql = $@"select * from CLASSIC_MENU where PARENT_NODE={next} order by PAGE_ID";
                return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
            }
            else if (prev != null)
            {

                //FormattableString sql = $@"select * from CLASSIC_MENU where MENU_ID={prev}";
                FormattableString sql = $@"select PM.*
                                        from CLASSIC_MENU PM
                                        JOIN CLASSIC_MENU CM on PM.PARENT_NODE=CM.PARENT_NODE
                                        where CM.MENU_ID={prev} order by PM.PAGE_ID";
                return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
            }
            else
            {

                FormattableString sql = $@"select * from CLASSIC_MENU where PARENT_NODE='N' order by MENU_ID";
                return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
            }
        }

        public List<CLASSIC_MENU> GetAll()
        {
            FormattableString sql = $@"select * from CLASSIC_MENU order by MENU_ID";
            return dbCtx.Database.SqlQuery<CLASSIC_MENU>(sql).ToList();
        }
    }
}
