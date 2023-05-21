using Microsoft.EntityFrameworkCore;
using Parcial_SaraMelissaMarroquinVega.DAL.Entities;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace Parcial_SaraMelissaMarroquinVega.DAL
{
    /// <summary>
    /// Esta clase se utiliza para mapear las entidades (tablas) en la base de datos 
    /// </summary>
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
