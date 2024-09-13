using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Helper
{
    public class RazorSanitizer
    {
        public static string Create<T>() where T : class
        {
            Type type = typeof(T);
            var objectName = type.Name;
            var properties = type.GetProperties();
            var sb = new StringBuilder();

            sb.AppendLine($"<div class=\"row\">\n");

            foreach (var prop in properties)
            {
                sb.AppendLine($"<div class=\"col-md-4\">");
                sb.AppendLine($"<label asp-for=\"{prop.Name}\" class=\"control-label\"></label>");
                if (prop.Name.EndsWith("_ID"))
                {
                    sb.AppendLine($" <select asp-for=\"{prop.Name}\" asp-items=\"@ViewBag.{prop.Name}\" class=\"form-control form-control-sm\">\r\n                    </select>");
                }
                else
                {
                    sb.AppendLine($"<input asp-for=\"{prop.Name}\" type=\"text\" class=\"form-control form-control-sm\" autocomplete=\"off\" placeholder=\"{prop.Name}\">");
                }
                sb.AppendLine($"<span asp-validation-for=\"{prop.Name}\" class=\"text-danger\"></span>");
                sb.AppendLine($"</div>");
            }
            sb.AppendLine($"</div>");
            return sb.ToString();
        }


        public static string Select<T>() where T : class
        {
            Type type = typeof(T);
            var objectName = type.Name;
            var properties = type.GetProperties();
            var sb = new StringBuilder();

            foreach (var prop in properties)
            {
                sb.AppendLine($"<th>{prop.Name}</th>");
            }
            foreach (var prop in properties)
            {
                sb.AppendLine($"<td>\r\n                            @Html.DisplayFor(modelItem => item.{prop.Name})\r\n                        </td>");
            }
            return sb.ToString();
        }
    }
}
