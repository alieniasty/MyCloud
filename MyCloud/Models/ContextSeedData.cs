using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyCloud.Models
{
    public class ContextSeedData
    {
        private CloudContext _context;
        private UserManager<CloudUser> _manager;

        public ContextSeedData(CloudContext context, UserManager<CloudUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        public async Task EnsureSeedData()
        {
            if (await _manager.FindByEmailAsync("test@domain.com") == null)
            {
                var user = new CloudUser()
                {
                    UserName = "Test",
                    Email = "test@domain.com"
                };

                await _manager.CreateAsync(user, "P@ssw0rd!!!%");

            }
        }
    }
}
