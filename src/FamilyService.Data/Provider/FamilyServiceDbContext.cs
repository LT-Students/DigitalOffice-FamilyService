using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using LT.DigitalOffice.FamilyService.Models.Db;
using System.Reflection;

namespace LT.DigitalOffice.FamilyService.Data.Provider
{
  /// <summary>
  /// A class that defines the tables and its properties in the database of FamilyService.
  /// </summary>
  public class FamilyServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbChild> Children {get; set;}
    public DbSet<DbParent> Parents {get; set;}

    public FamilyServiceDbContext(DbContextOptions<FamilyServiceDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.FamilyService.Models.Db"));
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;

      return Entry(obj).State;
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
    }

    void IBaseDataProvider.Save()
    {
      SaveChanges();
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }
  }
}