using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MassTransit;
using Microsoft.Extensions.Logging;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Models.Broker.Common;
using LT.DigitalOffice.FamilyService.Broker.Requests.Interfaces;

namespace LT.DigitalOffice.FamilyService.Broker.Requests
{
  public class UserService : IUserService
  {
    private readonly ILogger<UserService> _logger;
    private readonly IRequestClient<ICheckUsersExistence> _rcCheckUsersExistence;

    public UserService(
      ILogger<UserService> logger,
      IRequestClient<ICheckUsersExistence> rcCheckUsersExistence)
    {
      _logger = logger;
      _rcCheckUsersExistence = rcCheckUsersExistence;
    }
    
    public async Task<List<Guid>> CheckUsersExistenceAsync(List<Guid> parentUsersIds, List<string> errors = null)
    {
      if (parentUsersIds is null || !parentUsersIds.Any())
      {
        return null;
      }
      
      return
        (await RequestHandler.ProcessRequest<ICheckUsersExistence, ICheckUsersExistence>(
          _rcCheckUsersExistence,
          ICheckUsersExistence.CreateObj(parentUsersIds),
          errors,
          _logger))
        ?.UserIds;
    }
  }
}