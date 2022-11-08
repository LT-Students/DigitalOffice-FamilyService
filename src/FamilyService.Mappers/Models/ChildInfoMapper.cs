using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Enums;
using LT.DigitalOffice.FamilyService.Models.Dto.Models;
using LT.DigitalOffice.FamilyService.Mappers.Models.Interfaces;

namespace LT.DigitalOffice.FamilyService.Mappers.Models
{
  public class ChildInfoMapper : IChildInfoMapper
  {
    public ChildInfo Map(DbChild dbChild)
    {
      return dbChild is null ? default : new ChildInfo
      {
        Name = dbChild.Name,
        Gender = (Gender)dbChild.Gender,
        DateOfBirth = dbChild.DateOfBirth,
        Info = dbChild.Info
      };
    }
  }
}
