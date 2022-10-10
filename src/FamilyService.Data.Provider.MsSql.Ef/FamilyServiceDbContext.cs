using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.Kernel.EFSupport.Provider;

namespace LT.DigitalOffice.FamilyService.Data.Provider.MsSql.Ef
{
  /// <summary>
  /// A class that defines the tables and its properties in the database of FamilyService.
  /// </summary>
  public class FamilyServiceDbContext : DbContext, IDataProvider
  {
    public FamilyServiceDbContext(DbContextOptions<FamilyServiceDbContext> options)
      : base(options)
    {
    }

    //public DbSet<DbMembers> Members{get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;

      return Entry(obj).State;
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
    }
  }
}