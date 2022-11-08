using FluentValidation;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;

namespace LT.DigitalOffice.FamilyService.Validation.Child
{
  public class FindChildrenFilterValidator : AbstractValidator<FindChildrenFilter>, IFindChildrenFilterValidator
  {
    public FindChildrenFilterValidator()
    {
      When(x => x.LowerAgeLimit != null , () => {
        RuleFor(f => f.LowerAgeLimit)
          .Cascade(CascadeMode.Stop)
          .NotNull()
          .InclusiveBetween(0, 17)
          .WithMessage("Age must be between 0 (inclusive) and 17 (inclusive).");
      });
      
      When(x => x.UpperAgeLimit != null , () => {
        RuleFor(f => f.UpperAgeLimit)
          .Cascade(CascadeMode.Stop)
          .NotNull()
          .InclusiveBetween(0, 17)
          .WithMessage("Age must be between 0 (inclusive) and 17 (inclusive)."); 
      });
      
      When(x => (x.LowerAgeLimit != null && x.UpperAgeLimit != null), () => {
        RuleFor(f => f)
          .Must(f => f.LowerAgeLimit < f.UpperAgeLimit)
          .WithMessage("Lower age limit must be less than upper age limit.");
      });
    }
  }
}