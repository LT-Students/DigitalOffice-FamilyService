﻿using System;
using System.ComponentModel.DataAnnotations;
using LT.DigitalOffice.FamilyService.Models.Dto.Enums;

namespace LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child
{
  public class CreateChildRequest
  {
    public Guid ParentUserId { get; set; }
    [Required]
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Info { get; set; }
  }
}
