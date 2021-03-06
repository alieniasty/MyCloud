﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloud.Models
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string SharingUrl { get; set; }

        public ICollection<FileData> FileDatas { get; set; }
    }
}
