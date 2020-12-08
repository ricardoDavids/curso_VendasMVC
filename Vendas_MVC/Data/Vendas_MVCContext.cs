using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vendas_MVC.Models;

namespace Vendas_MVC.Data
{
    public class Vendas_MVCContext : DbContext
    {
        public Vendas_MVCContext (DbContextOptions<Vendas_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Vendas_MVC.Models.Department> Department { get; set; }
    }
}
