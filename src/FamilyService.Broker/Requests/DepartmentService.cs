using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MassTransit;
using Microsoft.Extensions.Logging;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Models.Broker.Requests.Department;
using LT.DigitalOffice.Models.Broker.Responses.Department;
using LT.DigitalOffice.FamilyService.Broker.Requests.Interfaces;

namespace LT.DigitalOffice.FamilyService.Broker.Requests
{
  public class DepartmentService : IDepartmentService
  {
    private readonly ILogger<DepartmentService> _logger;
    private readonly IRequestClient<IGetDepartmentsUsersRequest> _rcGetDepartmentsUsers;

    public DepartmentService(
      ILogger<DepartmentService> logger,
      IRequestClient<IGetDepartmentsUsersRequest> rcGetDepartmentsUsers)
    {
      _logger = logger;
      _rcGetDepartmentsUsers = rcGetDepartmentsUsers;
    }

    public async Task<List<Guid>> GetDepartmentUserAsync(List<Guid> departmentIds, List<string> errors)
    {
      return
        (await RequestHandler.ProcessRequest<IGetDepartmentsUsersRequest, IGetDepartmentsUsersResponse>(
          _rcGetDepartmentsUsers,
          IGetDepartmentsUsersRequest.CreateObj(departmentIds, null),
          errors,
          _logger))
        ?.Users
        .Where(u => u.IsActive)
        .Select(u => u.UserId).ToList();
    }
  }
}