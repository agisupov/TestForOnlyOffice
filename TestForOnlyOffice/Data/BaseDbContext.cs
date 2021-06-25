using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForOnlyOffice.Data
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() { }
        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
