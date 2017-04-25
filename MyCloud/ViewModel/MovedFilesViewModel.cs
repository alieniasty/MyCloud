using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloud.ViewModel
{
    public class MovedFilesViewModel
    {
        public List<string> Codes { get; set; }
        
        public string CurrentFolder { get; set; }
        
        public string NewFolder { get; set; }

    }
}
