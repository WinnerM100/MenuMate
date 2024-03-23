using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Context;

class ClientContext : DbContext
{
    public SqlConnector connector { get; set; }

    public DbSet<Client> clients { get; set; }

    public ClientContext(SqlConnector newConnector) : base(newConnector.DbConnection.ConnectionString)
    {
        connector = newConnector;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        modelBuilder.Entity<Client>().ToTable("Client");
    }
}