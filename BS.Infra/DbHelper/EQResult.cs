namespace BS.Infra.DbHelper
{
    public class EQResult
    {
        public EQResult()
        {
            Success = false;
            Rows = 0;
            Messages = "Messages";
            Entities = "Entities";
        }
        public bool Success { get; set; }
        public int Rows { get; set; }
        public string Messages { get; set; }
        public string Entities { get; set; }
    }
}
