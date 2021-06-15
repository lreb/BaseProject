using BaseProjectAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectAPI.Persistence
{
    public class BaseDataContext : DbContext
    {
        public BaseDataContext(DbContextOptions<BaseDataContext> options) : base (options)
        {

        }

        public DbSet<Item> Items { get; set; }
    }
}
