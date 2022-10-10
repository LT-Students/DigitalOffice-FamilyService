using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;

namespace LT.DigitalOffice.FamilyService.Validation.Child.Interfaces
{
  [AutoInject]
  public interface ICreateChildRequestValidator : IValidator<CreateChildRequest>
  {
  }
}
