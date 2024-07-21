using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Infra.Services.Utility
{
    public class AlertifyJsService
    {
        public static string SaveSuccess()
        {
            return $@"alertify.success('Record has been saved successfully!');";
        }
        public static string EditSuccess()
        {
            return $@"alertify.success('Record has been updated successfully!','success');";
        }
    }
}
