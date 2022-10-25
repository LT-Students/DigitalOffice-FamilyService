using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.FamilyService.Models.Dto.Enums;

namespace LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters
{
  public record FindChildrenFilter : BaseFindFilter
  {
    [FromQuery(Name = "parentuserid")]
    public Guid? ParentUserId { get; set; }

    [FromQuery(Name = "loweragelimit")]
    public DateTime? LowerAgeLimit { get; set; }

    [FromQuery(Name = "upperagelimit")]
    public DateTime? UpperAgeLimit { get; set; }

    [FromQuery(Name = "gender")]
    public Gender? Gender { get; set; }

    [FromQuery(Name = "department")]
    public List<Guid> Department { get; set; }
  }
}
