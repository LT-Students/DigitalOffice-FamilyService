using System;
using System.Text.RegularExpressions;
using FluentValidation;
using LT.DigitalOffice.FamilyService.Data.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests;
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;

namespace LT.DigitalOffice.FamilyService.Validation.Child
{
  public class CreateChildRequestValidator : AbstractValidator<CreateChildRequest>, ICreateChildRequestValidator
  {
    private static Regex NameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+)$");

    public CreateChildRequestValidator(
      IChildRepository _userRepository)
    {
      RuleFor(r => r.Name)
        .Cascade(CascadeMode.Stop)
        .MinimumLength(1).WithMessage("Minimum lenght is 1.")
        .MaximumLength(45).WithMessage("Maximum lenght is 45.")
        .Must(n => NameRegex.IsMatch(n.Trim()))
        .WithMessage("Name contains invalid characters.");

      RuleFor(r => r.Gender).IsInEnum()
        .WithMessage("This gender is not specified.");

      RuleFor(r => r.DateOfBirth)
        .Must(d => (DateTime.UtcNow.Year - d.Year) <= 18)
        .WithMessage("Age mustn't be greater than 18.");

      RuleFor(r => r.Info)
        .MinimumLength(0).WithMessage("Minimum lenght is 0.")
        .MaximumLength(300).WithMessage("Maximum lenght is 300.");

      RuleFor(r => r)
        .MustAsync(async (r, _, _) => !await _userRepository
        .DoesValueExist(r.ParentUserId, r.Name, r.DateOfBirth.Date))
        .WithMessage("This child already exists.");
    }
  }
}
