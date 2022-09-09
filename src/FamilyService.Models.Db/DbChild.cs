using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FamilyService.Models.Db
{
  public partial class DbChild
  {
    public const string TableName = "Children";

    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public int Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string Info { get; set; }
    public Guid ParentId { get; set; }
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
