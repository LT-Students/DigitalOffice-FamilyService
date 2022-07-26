﻿using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using FluentValidation.Results;
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
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;

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
    private readonly IFindChildrenFilterValidator _validator;

    public FindChildrenCommand(
      IChildRepository childRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IAccessValidator accessValidator,
      IChildInfoMapper childInfoMapper,
      IDepartmentService departmentService,
      IFindChildrenFilterValidator validator)
    {
      _childRepository = childRepository;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _accessValidator = accessValidator;
      _childInfoMapper = childInfoMapper;
      _departmentService = departmentService;
      _validator = validator;
    }

    public async Task<FindResultResponse<ChildInfo>> ExecuteAsync(FindChildrenFilter filter)
    {
      if (!(_httpContextAccessor.HttpContext.GetUserId() == filter.ParentUserId
        || await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)))
      {
        return _responseCreator.CreateFailureFindResponse<ChildInfo>(HttpStatusCode.Forbidden);
      }
      
      ValidationResult validationResult = await _validator.ValidateAsync(filter);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureFindResponse<ChildInfo>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }
      
      FindResultResponse<ChildInfo> response = new();

      List<Guid> departmentsUsers = filter.Departments is not null
        ? await _departmentService.GetDepartmentUserAsync(
          filter.Departments,
          response.Errors)
        : null;
      
      (List<DbChild> dbChildren, int totalCount) = await _childRepository.FindAsync(filter, departmentsUsers);

      response.Body = dbChildren?.Select(_childInfoMapper.Map).ToList();
      response.TotalCount = totalCount;
      
      return  response;
    }
  }
}
