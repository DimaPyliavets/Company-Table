using CompTable.Model;
using Microsoft.EntityFrameworkCore;

namespace CompTable.Data
{
    public class TableModelContext : DbContext
    {
        public TableModelContext(DbContextOptions<TableModelContext> options) : base(options) { }

        public DbSet<TableModel> TableModels { get; set; }
    }
}
