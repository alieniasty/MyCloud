using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloud.Models
{
    public class FileData
    {
        [Key]
        public string Name { get; set; }
        
        [Required]
        public string Base64Code { get; set; }

        public string Folder { get; set; }

        public string SharingUrl { get; set; }
    }
}
