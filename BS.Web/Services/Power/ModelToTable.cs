using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace BS.Web.Services.Power
{
    public class ModelToTable
    {
        public static string GenerateCreateTableQuery<T>() where T : class
        {
            Type type = typeof(T);
            var tableName = type.Name;
            var properties = type.GetProperties();
            var sb = new StringBuilder();

            sb.AppendLine($"CREATE TABLE [{tableName}] (");

            foreach (var prop in properties)
            {
                if (IsNotMapped(prop))
                {
                    continue;
                }
                var columnName = prop.Name;
                var columnType = GetSqlDataType(prop);
                var isNullable = IsNullable(prop);
                var isPrimaryKey = IsPrimaryKey(prop);
                var isRequired = IsRequired(prop);
                var stringLength = GetStringLength(prop);

                sb.Append($"    [{columnName}] {columnType}");

                if (!string.IsNullOrEmpty(stringLength))
                {
                    sb.Append($"({stringLength})");
                }

                if ((!isNullable && !isPrimaryKey) || isRequired)
                {
                    sb.Append(" NOT NULL");
                }
                if (isPrimaryKey)
                {
                    sb.Append(" PRIMARY KEY");
                }

                sb.AppendLine(",");
            }

            sb.Length--; // Remove last comma
            sb.AppendLine(");");

            return sb.ToString();
        }

        private static string GetSqlDataType(PropertyInfo prop)
        {
            var type = prop.PropertyType;

            if (type == typeof(int) || type == typeof(int?)) return "INT";
            if (type == typeof(string)) return "NVARCHAR";
            if (type == typeof(DateTime) || type == typeof(DateTime?)) return "DATETIME";
            if (type == typeof(bool) || type == typeof(bool?)) return "BIT";
            if (type == typeof(decimal) || type == typeof(decimal?)) return "DECIMAL(18, 4)";
            if (type == typeof(double) || type == typeof(double?)) return "FLOAT";
            if (type == typeof(float) || type == typeof(float?)) return "REAL";
            if (type == typeof(byte) || type == typeof(byte?)) return "TINYINT";
            if (type == typeof(Byte[]) || type == typeof(Byte?)) return "TIMESTAMP";

            return null!;
            //throw new NotSupportedException($"Type {type.Name} is not supported.");
        }

        private static bool IsNullable(PropertyInfo prop)
        {
            return Nullable.GetUnderlyingType(prop.PropertyType) != null || !prop.PropertyType.IsValueType;
        }

        private static bool IsPrimaryKey(PropertyInfo prop)
        {
            return prop.Name == "ID" || Attribute.IsDefined(prop, typeof(KeyAttribute));
        }

        private static bool IsRequired(PropertyInfo prop)
        {
            return prop.Name == "ID" || Attribute.IsDefined(prop, typeof(RequiredAttribute));
        }

        private static string GetStringLength(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttribute<StringLengthAttribute>();
            return attr != null ? attr.MaximumLength.ToString() : string.Empty;
        }
        private static bool IsNotMapped(PropertyInfo prop)
        {
            return Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute));
        }
    }
}
