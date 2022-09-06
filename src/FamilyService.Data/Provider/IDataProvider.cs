using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using LT.DigitalOffice.FamilyService.Models.Db;

namespace LT.DigitalOffice.FamilyService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbChild> Children { get; set;}
    DbSet<DbParent> Parents { get; set; }
  }
}