using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.FamilyService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IDepartmentService
  {
    Task<List<Guid>> GetDepartmentUserAsync(List<Guid> departmentIds, List<string> errors);
  }
}