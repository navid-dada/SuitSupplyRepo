using Microsoft.EntityFrameworkCore;

namespace SuitSupply.Order
{
    public class SuitSupplyContext:DbContext
    {
        public SuitSupplyContext(DbContextOptions option):base(option)
        {
        }

        public DbSet<Domain.Order> Orders { get; set; }
    }
}