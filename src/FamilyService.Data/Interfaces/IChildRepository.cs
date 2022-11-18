using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;

namespace LT.DigitalOffice.FamilyService.Data.Interfaces
{
  [AutoInject]
  public interface IChildRepository
  {
    Task<Guid?> CreateAsync(DbChild dbChild);
    Task<bool> DoesValueExist(Guid parentUserId, string name, DateTime dateOfBirth);
    Task<DbChild> GetAsync(Guid childId);
    Task<(List<DbChild> dbChildren, int totalCount)> FindAsync(FindChildrenFilter filter, List<Guid> departmentsUsers);
    Task<bool> EditAsync(DbChild dbChild, JsonPatchDocument<DbChild> request);
  }
}
