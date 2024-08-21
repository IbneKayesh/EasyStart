namespace BS.DMO.Models.Admin
{
    public class DATABASE_BACKUP_RESTORE
    {
        public string? DBR_PATH { get; set; }
        public string? FILE_NAME { get; set; }
        public DateTime FILE_TIME { get; set; }
        public string? FILE_SIZE { get; set; }
    }
}
