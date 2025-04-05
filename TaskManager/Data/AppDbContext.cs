using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskManager.Entities;

namespace TaskManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        AdjustNamesForSqlStandard(modelBuilder);
    }


    /// <summary>
    /// change the names of the attributes and tables to the form: string_str_text
    /// </summary>
    /// <param name="modelBuilder">table models</param>
    private static void AdjustNamesForSqlStandard(ModelBuilder modelBuilder)
    {

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Replace table names
            var tableName = entity?.GetTableName()?.ToSnakeCase();
            entity?.SetTableName(tableName);
            // Replace column names
            if (entity != null)
            {
                foreach (var property in entity.GetProperties())
                {
                    string? columnName = tableName != null ? property?.GetColumnName(StoreObjectIdentifier.Table(tableName, null))?.ToSnakeCase() : null;
                    property?.SetColumnName(columnName);
                }
                foreach (var key in entity.GetKeys())
                    key.SetName(key?.GetName()?.ToSnakeCase());

                foreach (var key in entity.GetForeignKeys())
                    key.SetConstraintName(key.GetConstraintName()?.ToSnakeCase());

                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(index?.Name?.ToSnakeCase());
            }
        }
    }
}

static partial class NameConventions
{


    [GeneratedRegex("^_")]
    private static partial Regex UnderscoreRegex();

    [GeneratedRegex("(?:(?<l>[a-z0-9])(?<r>[A-Z])|(?<l>[A-Z])(?<r>[A-Z][a-z0-9]))")]
    private static partial Regex AlphanumericRegex();

    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        var noLeadingUndescore = UnderscoreRegex().Replace(input, "");
        return AlphanumericRegex().Replace(noLeadingUndescore, "${l}_${r}").ToLower();
    }
}