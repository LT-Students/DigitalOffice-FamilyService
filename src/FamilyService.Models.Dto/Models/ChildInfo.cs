using System;
using LT.DigitalOffice.FamilyService.Models.Dto.Enums;

namespace LT.DigitalOffice.FamilyService.Models.Dto.Models
{
  public class ChildInfo
  {
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Info { get; set; }
  }
}
