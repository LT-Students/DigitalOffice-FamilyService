using LT.DigitalOffice.FamilyService.Models.Dto.Enums;

namespace LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child
{
  public record EditChildRequest
  {
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }
    public string Info { get; set; }
  }
}