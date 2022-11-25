using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentValidation;
using LT.DigitalOffice.FamilyService.Broker.Requests.Interfaces;
using LT.DigitalOffice.FamilyService.Data.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;

namespace LT.DigitalOffice.FamilyService.Validation.Child
{
  public class CreateChildRequestValidator : AbstractValidator<CreateChildRequest>, ICreateChildRequestValidator
  {
    private static Regex NameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+)$");

    public CreateChildRequestValidator(
      IChildRepository childRepository,
      IUserService userService)
    {
      RuleFor(r => r.ParentUserId)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("The parent's identifier can't be empty.")
        .MustAsync(async (parentUserId, _) =>
          (await userService.CheckUsersExistenceAsync(new List<Guid> { parentUserId }))?.Count == 1)
        .WithMessage("This parent user doesn't exist.");
      
      RuleFor(r => r.Name)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Child's name can't be empty.")
        .MaximumLength(45).WithMessage("Maximum lenght is 45.")
        .Must(n => NameRegex.IsMatch(n.Trim()))
        .WithMessage("Name contains invalid characters.");

      RuleFor(r => r.Gender).IsInEnum()
        .WithMessage("This gender is not specified.");

      RuleFor(r => r.DateOfBirth)
        .Must(d => d > DateTime.UtcNow.AddYears(-18))
        .WithMessage("Age should be less than 18.");

      RuleFor(r => r.Info)
        .MaximumLength(300).WithMessage("Maximum lenght is 300.");

      RuleFor(r => r)
        .MustAsync(async (r, _, _) => 
          !await childRepository.DoesValueExist(r.ParentUserId, r.Name, r.DateOfBirth.Date))
        .WithMessage("This child already exists.");
    }
  }
}
