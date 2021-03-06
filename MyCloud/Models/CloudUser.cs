﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyCloud.Models
{
    public class CloudUser : IdentityUser
    {
        public DateTime Registered;

        public long TotalSpace { get; set; }

        public long UsedSpace { get; set; }

        public long RemainingSpace { get; set; }

        public ICollection<Folder> Folders { get; set; }
    }
}
