﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.FamilyService.Models.Db
{
  public class DbChild
  {
    public const string TableName = "Children";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Info { get; set; }
    public Guid ParentUserId { get; set; }
    public bool IsActive { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
  }

  public class DbChildConfiguration : IEntityTypeConfiguration<DbChild>
  {
    public void Configure(EntityTypeBuilder<DbChild> builder)
    {
      builder
        .ToTable(DbChild.TableName);

      builder
        .HasKey(x => x.Id);

      builder
        .Property(x => x.Name)
        .IsRequired();
    }
  }
}
