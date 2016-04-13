using DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Context : DbContext
    {
        public Context()
            : base()
        {

        }
        public DbSet<Course> courses { get; set; }
        public DbSet<User> users { get; set; }
    }
}
