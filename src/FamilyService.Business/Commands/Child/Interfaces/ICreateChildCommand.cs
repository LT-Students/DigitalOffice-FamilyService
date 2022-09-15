using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests;

namespace LT.DigitalOffice.FamilyService.Business.Commands.Child.Interfaces
{
  [AutoInject]
  public interface ICreateChildCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateChildRequest request);
  }
}
