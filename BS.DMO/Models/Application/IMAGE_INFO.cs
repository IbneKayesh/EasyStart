using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Application
{
    public class IMAGE_INFO
    {
        public IMAGE_INFO()
        {
            ID = Guid.NewGuid().ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        public int TABLE_NAME { get; set; }
        public int COLUMN_NAME { get; set; }
        public int UPLOAD_PATH { get; set; }
        public int FILE_EXT { get; set; }
        public int FILE_SIZE { get; set; }

    }
}
