# Models Directory

This directory contains the Entity Framework Core models and database context for the Old School RuneScape Project. This README provides guidance on creating new models, configuring relationships, and managing database migrations.

## Overview

The project uses Entity Framework Core with PostgreSQL as the database provider. All database entities are defined in this directory and registered in the `ApplicationDbContext`.

## Current Models

### TaskList
Located in `TaskList.cs` - Represents task lists in the application.

**Properties:**
- `Id` (long) - Primary key
- `UserId` (Guid?) - Optional foreign key to user
- `Title` (string) - Required title of the task list

### User
Defined in `ApplicationDbContext.cs` - Basic user model.

**Properties:**
- `Id` (int) - Primary key
- `Username` (string) - User's username
- `Email` (string) - User's email address
- `CreatedAt` (DateTime) - Account creation timestamp

## Creating a New Model

### Step 1: Create the Model Class

Create a new `.cs` file in the `Models` directory:

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Old_School_Runescape_Project.Models;

[Table("your_table_name")] // Optional: specify custom table name
public class YourModel
{
    [Key] // Primary key
    [Column("id")] // Optional: specify custom column name
    public int Id { get; set; }

    [Required] // Not null constraint
    [MaxLength(100)] // String length constraint
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [EmailAddress] // Email validation
    public string Email { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties for relationships
    public virtual ICollection<RelatedModel>? RelatedItems { get; set; }
}
```

### Step 2: Register in DbContext

Add the model to `ApplicationDbContext.cs`:

```csharp
public DbSet<YourModel> YourModels { get; set; }
```

### Step 3: Configure with Fluent API (Optional)

Add configuration in the `OnModelCreating` method:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<YourModel>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
        entity.HasIndex(e => e.Email).IsUnique();
        
        // Configure relationships
        entity.HasMany(e => e.RelatedItems)
              .WithOne(r => r.YourModel)
              .HasForeignKey(r => r.YourModelId);
    });
}
```

### Step 4: Create and Apply Migration

```powershell
# Create migration
dotnet ef migrations add AddYourModelTable

# Apply migration to database
dotnet ef database update
```

## Common Data Annotations

### Validation Attributes
- `[Required]` - Field cannot be null
- `[MaxLength(n)]` - Maximum string length
- `[MinLength(n)]` - Minimum string length
- `[Range(min, max)]` - Numeric range validation
- `[EmailAddress]` - Email format validation
- `[Phone]` - Phone number format validation
- `[Url]` - URL format validation

### Database Mapping Attributes
- `[Key]` - Primary key
- `[Column("column_name")]` - Custom column name
- `[Table("table_name")]` - Custom table name
- `[DatabaseGenerated(DatabaseGeneratedOption.Identity)]` - Auto-increment
- `[ForeignKey("PropertyName")]` - Foreign key relationship

## Relationship Examples

### One-to-Many Relationship

```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation property
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Foreign key
    public int CategoryId { get; set; }
    
    // Navigation property
    public virtual Category Category { get; set; } = null!;
}
```

### Many-to-Many Relationship

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

// In OnModelCreating:
modelBuilder.Entity<Student>()
    .HasMany(s => s.Courses)
    .WithMany(c => c.Students)
    .UsingEntity(j => j.ToTable("StudentCourses"));
```

### One-to-One Relationship

```csharp
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    
    public virtual UserProfile Profile { get; set; } = null!;
}

public class UserProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}

// In OnModelCreating:
modelBuilder.Entity<UserProfile>()
    .HasOne(p => p.User)
    .WithOne(u => u.Profile)
    .HasForeignKey<UserProfile>(p => p.UserId);
```

## Migration Commands

```powershell
# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations to database
dotnet ef database update

# Rollback to a specific migration
dotnet ef database update PreviousMigrationName

# Remove last migration (if not applied)
dotnet ef migrations remove

# Generate SQL script for migrations
dotnet ef migrations script
```

## Best Practices

1. **Naming Conventions**
   - Use PascalCase for class names and properties
   - Use snake_case for database table/column names (via attributes)
   - Use descriptive, meaningful names

2. **Required Fields**
   - Always use `[Required]` for non-nullable reference types
   - Initialize string properties to `string.Empty` to avoid null reference warnings

3. **Navigation Properties**
   - Use `virtual` keyword for lazy loading
   - Initialize collections in property declarations or constructor
   - Use nullable reference types appropriately

4. **Indexes**
   - Add indexes on frequently queried columns
   - Consider unique indexes for business rules
   - Use composite indexes for multi-column queries

5. **Migrations**
   - Use descriptive migration names
   - Review generated migration code before applying
   - Backup database before applying migrations in production

## Environment Configuration

The project uses environment variables for database configuration. Ensure these are set:

- `DB_SERVER` - PostgreSQL server host
- `DB_PORT` - PostgreSQL server port (default: 5432)
- `DB_USER` - Database username
- `DB_PASSWORD` - Database password
- `DB_NAME` - Database name

## Troubleshooting

### Common Issues

1. **Migration Conflicts**
   ```powershell
   # Reset migrations (caution: data loss)
   dotnet ef database drop
   dotnet ef migrations remove
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

2. **Connection Issues**
   - Verify environment variables are set
   - Check PostgreSQL server is running
   - Validate connection string format

3. **Model Validation Errors**
   - Ensure required properties are set
   - Check data type compatibility
   - Verify foreign key relationships

For more detailed information, refer to the [Entity Framework Core documentation](https://docs.microsoft.com/en-us/ef/core/).