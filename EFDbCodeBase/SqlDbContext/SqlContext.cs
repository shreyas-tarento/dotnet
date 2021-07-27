using EFDbCodeBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDbCodeBase.SqlDbContext
{
    public class SqlContext:DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options):base(options)
        {
                
        }

        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}
