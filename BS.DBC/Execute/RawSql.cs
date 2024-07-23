namespace BS.DBC.Execute
{
    public class RawSql
    {
        private readonly AppDbContext _db;
        public RawSql(AppDbContext db)
        {
            _db = db;
        }

        public void ExecuteSqlCommand(List<SqlFormat> sqlFormat)
        {
            foreach (var item in sqlFormat)
            {
                _db.Database.ExecuteSqlRaw(item.Sql, item.parameters);
            }
            

            //RawSql rawSql = new RawSql(_db);
            //object[] pram = new object[] { };
            //List<string> sql_ls = new List<string>();
        }

        public void ExecuteSqlQuery<T>(SqlFormat sqlFormat)
        {
            _db.Database.SqlQueryRaw<T>(sqlFormat.Sql, sqlFormat.parameters);

            //RawSql rawSql = new RawSql(_db);
            //object[] pram = new object[] { };
            //List<string> sql_ls = new List<string>();
        }
        public static List<T> DatatableToModel<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }
        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
        }
    }
}
