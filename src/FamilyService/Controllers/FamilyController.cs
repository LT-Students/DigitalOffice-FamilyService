using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.FamilyService.Business.Commands.Child.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Dto.Models;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;

namespace LT.DigitalOffice.FamilyService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class FamilyController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateChildCommand command,
      [FromBody] CreateChildRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<ChildInfo>> FindAsync(
      [FromServices] IFindChildrenCommand command,
      [FromQuery] FindChildrenFilter filter)
    {
      return await command.ExecuteAsync(filter);
    }
  }
}
