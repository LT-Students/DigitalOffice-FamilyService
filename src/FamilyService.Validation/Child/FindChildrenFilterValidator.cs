using System.Threading;
using System.Globalization;
using FluentValidation;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;
using LT.DigitalOffice.FamilyService.Validation.Child.Resources;

namespace LT.DigitalOffice.FamilyService.Validation.Child
{
  public class FindChildrenFilterValidator : AbstractValidator<FindChildrenFilter>, IFindChildrenFilterValidator
  {
    public FindChildrenFilterValidator()
    {
      Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
      
      When(x => x.LowerAgeLimit != null , () => {
        RuleFor(f => f.LowerAgeLimit)
          .Cascade(CascadeMode.Stop)
          .NotNull()
          .InclusiveBetween(0, 17)
          .WithMessage($"{nameof(FindChildrenFilter.LowerAgeLimit)} {ChildValidatorResource.IncorrectAge}");
      });
      
      When(x => x.UpperAgeLimit != null , () => {
        RuleFor(f => f.UpperAgeLimit)
          .Cascade(CascadeMode.Stop)
          .NotNull()
          .InclusiveBetween(0, 17)
          .WithMessage($"{nameof(FindChildrenFilter.UpperAgeLimit)} {ChildValidatorResource.IncorrectAge}"); 
      });
      
      When(x => (x.LowerAgeLimit != null && x.UpperAgeLimit != null), () => {
        RuleFor(f => f)
          .Must(f => f.LowerAgeLimit < f.UpperAgeLimit)
          .WithMessage(ChildValidatorResource.IncorrectAgeRange);
      });
      
      RuleFor(f=> f.Gender)
        .IsInEnum().WithMessage($"{nameof(FindChildrenFilter.Gender)} {ChildValidatorResource.IsNotInEnum}");
    }
  }
}