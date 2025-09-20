# Migration Management Strategy

This document outlines the strategy for managing Entity Framework Core migrations in the OSRS Project.

## Current Approach

### Development Phase (Current)
- Keep all migrations during active development
- Squash migrations before major releases
- Remove migrations that haven't been deployed to production

### Production Deployment
- Never remove migrations that have been applied to production
- Always test migrations on a copy of production data first
- Create backup before applying migrations to production

## Migration Lifecycle

### 1. Development Migrations
```powershell
# Create feature migration
dotnet ef migrations add AddNewFeature

# Test locally
dotnet ef database update

# If changes needed, remove and recreate
dotnet ef migrations remove
dotnet ef migrations add AddNewFeatureRevised
```

### 2. Pre-Production Cleanup
Before deploying to production, consolidate development migrations:

```powershell
# Backup current database
# Then reset migrations (ONLY if not in production yet)
dotnet ef database drop --force
dotnet ef migrations remove --force  # Remove all
dotnet ef migrations add ConsolidatedMigration
dotnet ef database update
```

### 3. Production Migrations
Once in production, follow these rules:

```powershell
# NEVER remove migrations applied to production
# Create new migrations for changes
dotnet ef migrations add ProductionUpdate_$(Get-Date -Format "yyyyMMddHHmm")

# Generate SQL script for DBA review (recommended)
dotnet ef migrations script --from LastProductionMigration --to NewMigration --output migration.sql
```

## File Size Management

### Large Migration Files
When migrations become too large (>1MB), consider:

1. **Split Complex Migrations**
   ```csharp
   // Instead of one large migration, create multiple smaller ones
   dotnet ef migrations add AddBaseTables
   dotnet ef migrations add AddIndexes  
   dotnet ef migrations add AddConstraints
   dotnet ef migrations add SeedInitialData
   ```

2. **Data Migrations vs Schema Migrations**
   ```csharp
   // Schema migration (keep small)
   public partial class AddUserTable : Migration
   {
       protected override void Up(MigrationBuilder migrationBuilder)
       {
           migrationBuilder.CreateTable(
               name: "Users",
               columns: table => new { ... });
       }
   }
   
   // Separate data migration
   public partial class SeedUserData : Migration
   {
       protected override void Up(MigrationBuilder migrationBuilder)
       {
           // Large data inserts go here
       }
   }
   ```

3. **External SQL Files for Large Data**
   ```csharp
   protected override void Up(MigrationBuilder migrationBuilder)
   {
       var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                                  "Migrations", "Data", "large_data_insert.sql");
       var sql = File.ReadAllText(sqlFile);
       migrationBuilder.Sql(sql);
   }
   ```

## Directory Structure for Large Projects

```
Migrations/
├── README.md (this file)
├── Data/                          # Large SQL files
│   ├── large_data_insert.sql
│   └── seed_game_items.sql
├── 2025/
│   ├── Q1/                        # Quarterly organization
│   │   ├── 20250115_InitialCreate.cs
│   │   └── 20250301_AddTaskLists.cs
│   └── Q2/
│       └── 20250401_AddUserSystem.cs
└── Archive/                       # Old migrations (post-consolidation)
    └── pre_2025_migrations.txt    # Documentation of removed migrations
```

## Best Practices

### 1. Migration Naming
```powershell
# Good naming patterns
dotnet ef migrations add AddUserAuthenticationSystem
dotnet ef migrations add UpdateTaskListIndexes  
dotnet ef migrations add FixUserEmailConstraint

# Avoid vague names
dotnet ef migrations add Update1
dotnet ef migrations add Changes
dotnet ef migrations add Fix
```

### 2. Migration Content Guidelines
- **Keep schema changes atomic** - One logical change per migration
- **Add comments** for complex migrations
- **Test rollback scenarios** when possible
- **Document breaking changes** in migration comments

### 3. Code Review Checklist
- [ ] Migration name is descriptive
- [ ] Up() and Down() methods are consistent
- [ ] No hardcoded values (use configuration)
- [ ] Large data changes are separated from schema changes
- [ ] Breaking changes are documented
- [ ] Migration has been tested locally

## Rollback Strategy

### Safe Rollbacks
```csharp
// Always implement Down() method for rollbacks
protected override void Down(MigrationBuilder migrationBuilder)
{
    // Reverse the Up() operations
    migrationBuilder.DropTable("NewTable");
}
```

### Production Rollback
```powershell
# Rollback to specific migration
dotnet ef database update PreviousMigrationName

# Generate rollback script for DBA
dotnet ef migrations script CurrentMigration PreviousMigration --output rollback.sql
```

## Monitoring Migration Size

Create a PowerShell script to monitor migration file sizes:

```powershell
# Check migration file sizes
Get-ChildItem -Path "Migrations" -Filter "*.cs" | 
    Select-Object Name, @{Name="SizeKB";Expression={[math]::Round($_.Length/1KB,2)}} | 
    Sort-Object SizeKB -Descending
```

## When to Clean Up

### Safe to Remove (Development Only)
- Migrations not yet applied to production
- Migrations created in feature branches that were squashed
- Test migrations created during development

### Never Remove
- Migrations applied to production environment
- Migrations applied to staging/UAT environments
- Migrations that other team members have applied locally

## Emergency Procedures

### Corrupted Migration State
```powershell
# Check migration status
dotnet ef migrations list

# Manual migration table cleanup (DANGEROUS - backup first!)
# Connect to PostgreSQL and check __EFMigrationsHistory table
```

### Large Migration Causing Issues
1. **Stop the migration** if possible
2. **Rollback to previous stable state**
3. **Split the large migration** into smaller chunks
4. **Re-apply in smaller increments**

---

**Remember:** Always backup your database before applying migrations in production!

For questions or issues with migrations, refer to:
- [EF Core Migrations Documentation](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- Project team lead
- Database administrator (for production changes)