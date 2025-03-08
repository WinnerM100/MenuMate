using MenuMate.AccessLayer.Models;
using MenuMate.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuMate.AccessLayer.Context;

public class MenuMateContext : DbContext
{
    public DbSet<Client> Clients{ get; set; }
    public DbSet<User> Users{ get; set; }
    public DbSet<Role> Roles{ get; set; }

    private SqlConnector sqlConnector{ get; set; }

    public MenuMateContext(SqlConnector sqlConnector)
    {
        this.sqlConnector = sqlConnector;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(sqlConnector.DbConnection.ConnectionString);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("client");
        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<Role>().ToTable("Role");

        modelBuilder.Entity<Client>()
                    .HasOne(c => c.User)
                    .WithOne(u => u.Client)
                    .HasForeignKey<Client>(c => c.UserId);
                    
        modelBuilder.Entity<User>()
                    .HasOne(u => u.Client)
                    .WithOne(c => c.User)
                    .HasForeignKey<User>(u => u.ClientId);

        modelBuilder.Entity<User>()
                    .HasMany(u => u.Roles)
                    .WithMany(r => r.Users)
                    .UsingEntity("RoleUser");       
    }
}