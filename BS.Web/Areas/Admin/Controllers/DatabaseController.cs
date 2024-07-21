using BS.DMO.Models.Setup;
using BS.Infra.DbHelper;

namespace BS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DatabaseController : BaseController
    {

        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public DatabaseController(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            List<DATABASE_BACKUP_RESTORE> fileList = new List<DATABASE_BACKUP_RESTORE>();
            var appDatabasePath = Path.Combine(_env.WebRootPath, StaticKeys.BackupPath);
            DirectoryInfo di = new DirectoryInfo(appDatabasePath);
            FileInfo[] files = (from f in di.GetFiles("*.bak")
                                orderby f.LastWriteTime descending
                                select f).ToArray();
            foreach (var item in files)
            {
                DATABASE_BACKUP_RESTORE obj = new DATABASE_BACKUP_RESTORE();
                obj.FILE_NAME = item.Name;
                obj.FILE_TIME = item.CreationTime;
                obj.DBR_PATH = appDatabasePath;
                obj.FILE_SIZE = FormatSize(item.Length);
                fileList.Add(obj);
            }
            return View(fileList);
        }
        public IActionResult Create()
        {
            DateTime dateTime = DateTime.Now;
            string connectionString = _configuration.GetConnectionString(StaticKeys.ConnectionString);
            var appDatabasePath = Path.Combine(_env.WebRootPath, StaticKeys.BackupPath);
            string backupFilePath = string.Format(appDatabasePath + $"\\{StaticKeys.BackupFileName}_1_{0}_" + dateTime.ToString("dd_MMM_yyyy_hh_mm_ss") + ".bak", dateTime.Ticks);
            CreateBackup(backupFilePath, string.IsNullOrWhiteSpace(connectionString) ? "" : connectionString);
            return RedirectToAction(nameof(Index));
        }
        public void CreateBackup(string backupFilePath, string connectionString)
        {
            try
            {
                //mdf file default database name is "master"
                //string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={mdfFilePath};Integrated Security=True";

                // Create a new SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();
                    // Create a new SqlCommand
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        // Set the command text for the backup operation
                        command.CommandText = string.Format("BACKUP DATABASE {1} TO DISK='{0}' WITH FORMAT", backupFilePath, "bs_db");
                        // Execute the backup command
                        command.ExecuteNonQuery();
                        // Display a success message
                        TempData["msg"] = NotifyService.Success("Database backup created successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the backup operation
                TempData["msg"] = NotifyService.Error("Backup failed. Error: " + ex.Message);
            }
        }

        private string FormatSize(double len)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            //double len = new FileInfo(filename).Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = new EQResult();

            var appDatabasePath = Path.Combine(_env.WebRootPath, $"{StaticKeys.BackupPath}\\" + id);
            if (System.IO.File.Exists(appDatabasePath))
            {
                try
                {
                    System.IO.File.Delete(appDatabasePath);
                    eQResult.success = true;
                    eQResult.messages = NotifyService.DeletedSuccessString(id);
                }
                catch (Exception ex)
                {
                    eQResult.messages = ex.Message;
                }
            }
            else
            {
                eQResult.messages = NotifyService.NotFoundString();
            }
            return Json(eQResult);
        }
    }
}
