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
        
        // Adicionar as 3 Entidades
        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
