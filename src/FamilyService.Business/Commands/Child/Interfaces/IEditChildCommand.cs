using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;

namespace LT.DigitalOffice.FamilyService.Business.Commands.Child.Interfaces
{
  [AutoInject]
  public interface IEditChildCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid childId, JsonPatchDocument<EditChildRequest> request);
  }
}