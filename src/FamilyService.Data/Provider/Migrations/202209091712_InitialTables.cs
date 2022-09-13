using LT.DigitalOffice.FamilyService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.FamilyService.Data.Provider.Migrations
{
  [DbContext(typeof(FamilyServiceDbContext))]
  [Migration("202209091712_InitialTables")]
  class InitialTables : Migration
  {
    protected override void Up(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbChild.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false, maxLength: 45),
          Gender = table.Column<int>(nullable: false),
          DateOfBirth = table.Column<DateTime>(nullable: false),
          Info = table.Column<string>(nullable: false, maxLength: 300),
          ParentUserId = table.Column<Guid>(nullable: false),
          IsActive = table.Column<bool>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbChild.TableName}", x => x.Id);
        });
    }
  }
}
