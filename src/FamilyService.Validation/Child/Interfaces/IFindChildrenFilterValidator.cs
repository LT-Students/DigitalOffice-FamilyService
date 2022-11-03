using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;

namespace LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;

[AutoInject]
public interface IFindChildrenFilterValidator : IValidator<FindChildrenFilter>
{
}