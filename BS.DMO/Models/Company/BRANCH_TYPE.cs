namespace BS.DMO.Models.Company
{
    public class BRANCH_TYPE
{
    public BRANCH_TYPE()
    {
        ID = Guid.Empty.ToString();
    }
    public string ID { get; set; }

    public string? BRANCH_TYPE_NAME { get; set; }
    public string? SHORT_NAME { get; set; }
}
}