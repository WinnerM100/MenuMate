using MenuMate.AccessLayer.Models;
using MenuMate.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuMate.AccessLayer.Context;

public class MenuMateContext : DbContext
{
    public DbSet<Client> Clients{ get; set; }
    public DbSet<User> Users{ get; set; }

    private SqlConnector sqlConnector{ get; set; }

    public MenuMateContext(SqlConnector sqlConnector)
    {
        this.sqlConnector = sqlConnector;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(sqlConnector.DbConnection.ConnectionString);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("client");
        modelBuilder.Entity<User>().ToTable("user");
    }
}