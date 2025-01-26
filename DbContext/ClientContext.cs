using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MenuMate.Models;
using MenuMate.Utilities.Sql;
using Microsoft.EntityFrameworkCore;

namespace MenuMate.Context;

public class ClientContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SqlConnector connector { get; set; }

    public Microsoft.EntityFrameworkCore.DbSet<Client> clients { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<User> users { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Role> roles { get; set; }


    public ClientContext(SqlConnector newConnector) : base()
    {
        connector = newConnector;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   
        optionsBuilder.UseSqlServer(connector.DbConnection.ConnectionString);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("Client");
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<User>().ToTable("User");

        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>()
                    .HasOne(c => c.User);
                    //.WithOne(u => u.Client)
                    //.HasForeignKey<User>(u => u.ClientId)
                    //.IsRequired();

        modelBuilder.Entity<User>()
                    .HasMany(u => u.Roles)
                    .WithMany(r => r.Users)
                    .UsingEntity("dbo.RoleUser");
    }
}