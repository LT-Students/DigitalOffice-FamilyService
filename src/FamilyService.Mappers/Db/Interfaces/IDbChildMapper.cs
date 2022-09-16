using System;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;

namespace LT.DigitalOffice.FamilyService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbChildMapper
  {
    DbChild Map(CreateChildRequest request, Guid creatorId);
  }
}
