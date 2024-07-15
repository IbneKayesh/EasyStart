namespace BS.Infra.DbHelper
{
    public class NotifyServices
    {
        public static string Success()
        {
            return $"$.notify('Your request has been processed successfully.!','success');";
        }
        public static string Success(string text)
        {
            return $"$.notify('{text}','success');";
        }
        public static string Info()
        {
            return $"$.notify('Please wait while the task is being completed.','info');";
        }
        public static string Info(string text)
        {
            return $"$.notify('{text}','info');";
        }
        public static string Warning()
        {
            return $"$.notify('Please review the information carefully before proceeding.','warn');";
        }
        public static string Warning(string text)
        {
            return $"$.notify('{text}','warn');";
        }
        public static string Error()
        {
            return $"$.notify('An error occurred while processing request!','error');";
        }
        public static string Error(string text)
        {
            return $"$.notify('{text}','error');";
        }







        public static string NotFound()
        {
            return $@"$.notify('Data not found. The data may have been deleted!','warn');";
        }
        public static string DeletedSuccess(string id = "0")
        {
            return $@"$.notify('Record <{id}> has been deleted successfully!','success');";
        }
        public static string SaveSuccess()
        {
            return $@"$.notify('Record has been saved successfully!','success');";
        }
        public static string EditSuccess()
        {
            return $@"$.notify('Record has been updated successfully!','success');";
        }
        public static string EditRestricted()
        {
            return $@"$.notify('Edit is restricted or not available for edit!','warn');";
        }
        public static string DeleteHasChild(string childName, int childCount, string parentName)
        {
            return $"$.notify('{childCount} {childName} added with {parentName}, pls remove them first!','error');";
        }
    }
}
