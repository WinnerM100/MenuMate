using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Context;

public class AuthContext : DbContext
{
    public SqlConnector connector { get; set; }

    public DbSet<User> users { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<UsersRoles> usersRoles{ get; set; }

    public AuthContext(SqlConnector newConnector) : base(newConnector.DbConnection.ConnectionString)
    {
        connector = newConnector;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<User>()
                    .HasMany(user => user.Roles)
                    .WithMany(role => role.Users)
                    .Map(userRoleMapping =>
                        {
                            userRoleMapping.MapLeftKey("UserId");
                            userRoleMapping.MapRightKey("RoleId");
                            userRoleMapping.ToTable("UsersRoles");
                        });
    }
}