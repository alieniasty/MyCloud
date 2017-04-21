using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloud.ViewModel
{
    public class FileViewModel
    {
        [Required]
        public string Base64Code { get; set; }

        [Required]
        public string Folder { get; set; }
    }
}
