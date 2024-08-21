namespace BS.Web.Areas.HRMS.Model
{
    public class ViewPathFinder
    {
        public static string ViewName(Type controllerType, string ViewName)
        {
            // Extract the namespace
            string namespaceName = controllerType.Namespace;

            // Extract area name, folder name, and controller name
            string areaName = ExtractAreaName(namespaceName);
            string folderName = ExtractFolderName(namespaceName);
            string controllerName = ExtractControllerName(controllerType);

            return $"/Areas/{areaName}/Views/{folderName}/{controllerName}/{ViewName}.cshtml";
        }
        private static string ExtractAreaName(string namespaceName)
        {
            // Assuming the namespace format is like "BS.Web.Areas.HRMS.Controllers.Setup"
            // The area name is usually the segment after "Areas."
            var segments = namespaceName.Split('.');
            int areaIndex = Array.IndexOf(segments, "Areas") + 1;
            return areaIndex > 0 && areaIndex < segments.Length ? segments[areaIndex] : "Unknown";
        }

        private static string ExtractFolderName(string namespaceName)
        {
            // Assuming the namespace format is like "BS.Web.Areas.HRMS.Controllers.Setup"
            // The folder name is usually the segment after "Controllers."
            var segments = namespaceName.Split('.');
            int controllersIndex = Array.IndexOf(segments, "Controllers") + 1;
            return controllersIndex > 0 && controllersIndex < segments.Length ? segments[controllersIndex] : "Unknown";
        }
        private static string ExtractControllerName(Type controllerType)
        {
            // Controller name is the class name without "Controller" suffix
            string className = controllerType.Name;
            if (className.EndsWith("Controller"))
            {
                return className.Substring(0, className.Length - "Controller".Length);
            }
            return className;
        }
    }
}
