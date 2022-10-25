using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.FamilyService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IChildInfoMapper
  {
    ChildInfo Map(DbChild dbChild);
  }
}
