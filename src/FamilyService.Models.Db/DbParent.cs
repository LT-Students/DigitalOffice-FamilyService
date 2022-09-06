using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.FamilyService.Models.Db
{
  public class DbParent
  {
    public const string TableName = "Parents";

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }

    public ICollection<DbChild> Children { get; set; }

    public DbParent()
    {
      Children = new HashSet<DbChild>();
    }
  }

  public class DbParentConfiguration : IEntityTypeConfiguration<DbParent>
  {
    public void Configure(EntityTypeBuilder<DbParent> builder)
    {
      builder
        .ToTable(DbParent.TableName);

      builder
        .HasKey(x => x.Id);

      builder
        .Property(x => x.FirstName)
        .IsRequired();

      builder
        .Property(x => x.LastName)
        .IsRequired();

      builder
        .HasMany(p => p.Children)
        .WithOne(pc => pc.Parent);
    }
  }
}
