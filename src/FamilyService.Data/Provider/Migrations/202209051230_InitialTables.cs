using LT.DigitalOffice.FamilyService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.FamilyService.Data.Provider.Migrations
{
  [DbContext(typeof(FamilyServiceDbContext))]
  [Migration("202209051230_InitialTables")]
  class InitialTables : Migration
  {
    protected override void Up(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbChild.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          FirstName = table.Column<string>(nullable: false, maxLength: 45),
          LastName = table.Column<string>(nullable: false, maxLength: 45),
          Gender = table.Column<int>(nullable: false),
          BirthDate = table.Column<DateTime>(nullable: false),
          Info = table.Column<string>(nullable: false, maxLength: 300)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbChild.TableName}", x => x.Id);
        });

      builder.CreateTable(
        name: DbParent.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          FirstName = table.Column<string>(nullable: false, maxLength: 45),
          LastName = table.Column<string>(nullable: false, maxLength: 45),
          MiddleName = table.Column<string>(nullable: false, maxLength: 45)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbParent.TableName}", x => x.Id);
        });
    }
  }
}
