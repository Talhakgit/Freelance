using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class ProductContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Initial Catalog=ProductView;Trust Server Certificate=True;Integrated Security=True;");

        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        
    }
}
