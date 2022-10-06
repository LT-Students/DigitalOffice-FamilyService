using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FluentValidation.Results;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.FamilyService.Data.Interfaces;
using LT.DigitalOffice.FamilyService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;
using LT.DigitalOffice.FamilyService.Business.Commands.Child.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;

namespace LT.DigitalOffice.FamilyService.Business.Commands.Child
{
  public class CreateChildCommand : ICreateChildCommand
  {
    private readonly IChildRepository _childRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IAccessValidator _accessValidator;
    private readonly ICreateChildRequestValidator _validator;
    private readonly IDbChildMapper _dbChildMapper;

    public CreateChildCommand(
      IChildRepository childRepository,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IAccessValidator accessValidator,
      ICreateChildRequestValidator validator,
      IDbChildMapper dbChildMapper)
    {
      _childRepository = childRepository;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _accessValidator = accessValidator;
      _validator = validator;
      _dbChildMapper = dbChildMapper;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateChildRequest request)
    {
      if (!(_httpContextAccessor.HttpContext.GetUserId() == request.ParentUserId 
        || await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers)))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _childRepository.CreateAsync(_dbChildMapper.Map(request));

      if (response.Body is null)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
