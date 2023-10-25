using Microsoft.EntityFrameworkCore;
using CCA.API.Model;

namespace CCA.API.Data
{
    public class DataContext : DbContext
    {

        public DataContext( DbContextOptions<DataContext> options ) : base( options )
        {
        }

        public DbSet<CurrentAccountStatement> CurrentAccountStatement { get; set; }
    }
}
