using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.FamilyService.Business.Commands.Child.Interfaces;
using LT.DigitalOffice.FamilyService.Data.Interfaces;
using LT.DigitalOffice.FamilyService.Mappers.Patch.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;

namespace LT.DigitalOffice.FamilyService.Business.Commands.Child
{
  public class EditChildCommand : IEditChildCommand
  {
    private readonly IChildRepository _childRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IAccessValidator _accessValidator;
    private readonly IEditChildRequestValidator _validator;
    private readonly IPatchDbChildMapper _mapper;

    public EditChildCommand(
      IChildRepository childRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IAccessValidator accessValidator,
      IEditChildRequestValidator validator,
      IPatchDbChildMapper mapper)
    {
      _childRepository = childRepository;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _accessValidator = accessValidator;
      _validator = validator;
      _mapper = mapper;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid childId, JsonPatchDocument<EditChildRequest> request)
    {
      DbChild dbChild = await _childRepository.GetAsync(childId);

      if (dbChild is null)
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.NotFound);
      }

      if (!(dbChild.ParentUserId == _httpContextAccessor.HttpContext.GetUserId()
        || await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      return new() { Body = await _childRepository.EditAsync(dbChild, _mapper.Map(request)) };
    }
  }
}