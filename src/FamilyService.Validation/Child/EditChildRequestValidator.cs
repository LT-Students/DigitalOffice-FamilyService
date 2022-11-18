using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using LT.DigitalOffice.Kernel.Validators;
using LT.DigitalOffice.FamilyService.Models.Dto.Enums;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;
using LT.DigitalOffice.FamilyService.Validation.Child.Interfaces;

namespace LT.DigitalOffice.FamilyService.Validation.Child
{
  public class EditChildRequestValidator : BaseEditRequestValidator<EditChildRequest>, IEditChildRequestValidator
  {
    private static Regex NameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+[-\s`]?[a-zA-Zа-яА-ЯёЁ]+)$");
    
    private void HandleInternalPropertyValidation(
      Operation<EditChildRequest> requestedOperation,
      ValidationContext<JsonPatchDocument<EditChildRequest>> context)
    {
      RequestedOperation = requestedOperation;
      Context = context;

      AddСorrectPaths(
        new List<string> {
          nameof(EditChildRequest.Name),
          nameof(EditChildRequest.Gender),
          nameof(EditChildRequest.IsActive),
          nameof(EditChildRequest.Info)
        });

      AddСorrectOperations(nameof(EditChildRequest.Name), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditChildRequest.Gender), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditChildRequest.IsActive), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditChildRequest.Info), new List<OperationType> { OperationType.Replace });
      
      AddFailureForPropertyIf(
        nameof(EditChildRequest.Name),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditChildRequest>, bool>, string> {
          { x=> !string.IsNullOrEmpty(x.value?.ToString().Trim()), "Child's name mustn't be empty."},
          { x => NameRegex.IsMatch(x.value.ToString().Trim()), "Name contains invalid characters." },
          { x => x.value.ToString().Trim().Length <= 45, "Maximum name lenght is 45."}
        }, CascadeMode.Stop);
      
      AddFailureForPropertyIf(
        nameof(EditChildRequest.Gender),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditChildRequest>, bool>, string> {
          { x => Enum.TryParse(typeof(Gender), x.value?.ToString(), out _), "This gender is not specified."}
        });
      
      AddFailureForPropertyIf(
        nameof(EditChildRequest.IsActive),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditChildRequest>, bool>, string> {
          { x => bool.TryParse(x.value?.ToString(), out _), "Incorrect format IsActive"}
        });
      
      AddFailureForPropertyIf(
        nameof(EditChildRequest.Info),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditChildRequest>, bool>, string> {
          { x=> !string.IsNullOrEmpty(x.value?.ToString()), "Information mustn't be empty."},
          { x => x.value.ToString().Trim().Length <= 300, "Maximum information lenght is 300."}
        }, CascadeMode.Stop);
    }

    public EditChildRequestValidator()
    {
      RuleForEach(x => x.Operations)
        .Custom(HandleInternalPropertyValidation);
    }
  }
}