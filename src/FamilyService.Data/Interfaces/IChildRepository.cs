using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
    Task<(List<DbChild> dbChildren, int totalCount)> FindAsync(FindChildrenFilter filter, List<Guid> departmentsUsers);
  }
}
