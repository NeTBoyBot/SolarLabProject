using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastucture.DataAccess;

/// <summary>
/// Контекст БД
/// </summary>
public class BoardDbContext : DbContext
{
    
    /// <summary>
    /// Инициализирует экземпляр <see cref="BoardDbContext"/>.
    /// </summary>
    public BoardDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityUserRole<string>> UserRoles { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
    }
}