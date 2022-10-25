using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.FamilyService.Broker.Requests.Interfaces;
using LT.DigitalOffice.FamilyService.Business.Commands.Child.Interfaces;
using LT.DigitalOffice.FamilyService.Data.Interfaces;
using LT.DigitalOffice.FamilyService.Mappers.Models.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Models;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;

namespace LT.DigitalOffice.FamilyService.Business.Commands.Child
{
  public class FindChildrenCommand : IFindChildrenCommand
  {
    private readonly IChildRepository _childRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IAccessValidator _accessValidator;
    private readonly IChildInfoMapper _childInfoMapper;
    private readonly IDepartmentService _departmentService;

    public FindChildrenCommand(
      IChildRepository childRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IAccessValidator accessValidator,
      IChildInfoMapper childInfoMapper,
      IDepartmentService departmentService)
    {
      _childRepository = childRepository;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _accessValidator = accessValidator;
      _childInfoMapper = childInfoMapper;
      _departmentService = departmentService;
    }

    public async Task<FindResultResponse<ChildInfo>> ExecuteAsync(FindChildrenFilter filter)
    {
      if (!(await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)
        || _httpContextAccessor.HttpContext.GetUserId() == filter.ParentUserId))
      {
        return _responseCreator.CreateFailureFindResponse<ChildInfo>(HttpStatusCode.Forbidden);
      }

      FindResultResponse<ChildInfo> response = new();

      List<Guid> departmentsUsers = filter.Department is not null
        ? await _departmentService.GetDepartmentUserAsync(
          filter.Department,
          response.Errors)
        : null;
      
      (List<DbChild> dbChildren, int totalCount) = await _childRepository.FindAsync(filter, departmentsUsers);

      response.Body = new();
      
      if (dbChildren is not null)
      {
        response.Body.AddRange(dbChildren.Select(dbChild => _childInfoMapper.Map(dbChild)));

        response.TotalCount = totalCount;
      }

      return response;
    }
  }
}
