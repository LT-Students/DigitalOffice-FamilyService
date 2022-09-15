using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests;

namespace LT.DigitalOffice.FamilyService.Validation.Child.Interfaces
{
  [AutoInject]
  public interface ICreateChildRequestValidator : IValidator<CreateChildRequest>
  {
  }
}
