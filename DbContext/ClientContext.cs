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
    public Microsoft.EntityFrameworkCore.DbSet<UsersRoles> usersRoles{ get; set; }


    public ClientContext(SqlConnector newConnector) : base()
    {
        connector = newConnector;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   
        optionsBuilder.UseSqlServer(connector.DbConnection.ConnectionString);
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("Client");
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<User>().ToTable("User");

        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>()
                    .HasOne(c => c.User)
                    .WithOne(u => u.Client)
                    .HasForeignKey<User>(u => u.ClientId)
                    .IsRequired();

        modelBuilder.Entity<UsersRoles>()
                    .HasOne(ur => ur.User)
                    .WithMany(u => u.UsersRoles)
                    .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UsersRoles>()
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UsersRoles)
                    .HasForeignKey(ur => ur.RoleId);
    }
}