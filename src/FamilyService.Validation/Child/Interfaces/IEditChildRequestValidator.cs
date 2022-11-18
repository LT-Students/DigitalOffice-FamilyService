using FluentValidation;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.FamilyService.Validation.Child.Interfaces
{
  [AutoInject]
  public interface IEditChildRequestValidator : IValidator<JsonPatchDocument<EditChildRequest>>
  {
  }
}