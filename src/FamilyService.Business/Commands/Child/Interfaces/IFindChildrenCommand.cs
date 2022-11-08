using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Dto.Models;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;

namespace LT.DigitalOffice.FamilyService.Business.Commands.Child.Interfaces
{
  [AutoInject]
  public interface IFindChildrenCommand
  {
    Task<FindResultResponse<ChildInfo>> ExecuteAsync(FindChildrenFilter filter);
  }
}
