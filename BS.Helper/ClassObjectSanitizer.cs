using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Helper
{
    public class ClassObjectSanitizer
    {
        public static string Select<T>() where T : class
        {
            Type type = typeof(T);
            var objectName = type.Name;
            var properties = type.GetProperties();
            var sb = new StringBuilder();

            sb.AppendLine($"select new {objectName} \n{{");

            foreach (var prop in properties)
            {
                sb.AppendLine($"{prop.Name} = a.{prop.Name},");
            }
            sb.AppendLine($"\n}}");
            return sb.ToString();
        }
        public static string SetForSave<T>() where T : class
        {
            Type type = typeof(T);
            var objectName = type.Name;
            var properties = type.GetProperties();
            var sb = new StringBuilder();
            foreach (var prop in properties)
            {
                sb.AppendLine($"entity.{prop.Name} = obj.{prop.Name};");
            }
            return sb.ToString();
        }
    }
}
