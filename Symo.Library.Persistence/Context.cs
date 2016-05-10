using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Persistence
{
    public class Context : DbContext
    {
        public DbSet<KeyValueGroup> Configurations { get; set; }
    }
}
