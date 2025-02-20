using MassTransit;
using Microsoft.EntityFrameworkCore;
using TestTemplate14.Core.Entities;

namespace TestTemplate14.Data
{
    public class TestTemplate14DbContext : DbContext
    {
        public TestTemplate14DbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Foo> Foos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
