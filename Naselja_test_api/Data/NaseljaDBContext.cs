using Naselja_test_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Naselja_test_api.Data
{
    public class NaseljaDBContext : DbContext
    {
        public NaseljaDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Drzava> Drzava { get; set; }

        public DbSet<Naselje> Naselje { get; set; }
    }
}
