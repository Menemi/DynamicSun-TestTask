using DynamicSunTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicSunTestTask.Data;

public sealed class AppDbContext : DbContext
{
    private const string ConnectionString =
        "Host=localhost;Port=32768;Database=postgres;Username=postgres;Password=1753;";

    public DbSet<Weather> Weathers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(ConnectionString);
    }
}