using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.FamilyService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class FamilyController : ControllerBase
  {
    [HttpGet("get")]
    public void GetKidsAsync(
      [FromQuery] Guid employeeId)
    {

    }
    [HttpPost("post")]
    public void AddKidsAsync(
      [FromQuery] Guid employeeId)
    {

    }
  }
}
