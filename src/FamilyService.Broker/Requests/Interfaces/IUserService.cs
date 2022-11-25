using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.FamilyService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IUserService
  {
    Task<List<Guid>> CheckUsersExistenceAsync(List<Guid> parentUsersIds, List<string> errors = null);
  }
}