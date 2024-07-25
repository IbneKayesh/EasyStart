namespace BS.DMO.ViewModels.SalesOrder
{
    public class NEW_SB_VM
    {
        public SB_MASTER_VM SB_MASTER_VM { get; set; } = new SB_MASTER_VM();
        public List<SB_CHILD>? SB_CHILD { get; set; }
    }
}
