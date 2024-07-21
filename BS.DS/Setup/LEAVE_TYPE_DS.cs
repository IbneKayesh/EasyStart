using BS.DMO.Models.Setup;

namespace BS.DS.Setup
{
    public class LEAVE_TYPE_DS
    {
        public void Insert()
        {
            List<LEAVE_TYPE> list = new List<LEAVE_TYPE>();
            list.Add(new LEAVE_TYPE { ID = Guid.NewGuid().ToString(), LEAVE_TYPE_NAME = "Working Day", IS_WORKING_DAY = true });
            list.Add(new LEAVE_TYPE { ID = Guid.NewGuid().ToString(), LEAVE_TYPE_NAME = "Weekend", IS_WORKING_DAY = false });
        }
    }
}
