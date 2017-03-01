using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloud.Models
{
    public class CloudRepository : ICloudRepository
    {
        private CloudContext _context;

        public CloudRepository(CloudContext context)
        {
            _context = context;
        }
    }
}
