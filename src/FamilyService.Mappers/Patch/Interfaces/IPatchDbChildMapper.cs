using Microsoft.AspNetCore.JsonPatch;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;

namespace LT.DigitalOffice.FamilyService.Mappers.Patch.Interfaces
{
  [AutoInject]
  public interface IPatchDbChildMapper
  {
    JsonPatchDocument<DbChild> Map(JsonPatchDocument<EditChildRequest> request);
  }
}