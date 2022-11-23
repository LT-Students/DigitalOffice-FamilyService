using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using LT.DigitalOffice.FamilyService.Mappers.Patch.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Enums;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;

namespace LT.DigitalOffice.FamilyService.Mappers.Patch
{
  public class PatchDbChildMapper : IPatchDbChildMapper
  {
    public JsonPatchDocument<DbChild> Map(JsonPatchDocument<EditChildRequest> request)
    {
      if (request is null)
      {
        return null;
      }

      JsonPatchDocument<DbChild> patchDbChild = new();
      
      foreach (Operation<EditChildRequest> item in request.Operations)
      {
        if (item.path.EndsWith(nameof(EditChildRequest.Gender), StringComparison.OrdinalIgnoreCase))
        {
          patchDbChild.Operations.Add(new Operation<DbChild>(
            item.op, item.path, item.from, (int)Enum.Parse(typeof(Gender), item.value.ToString())));
          
          continue;
        }
        
        if (item.path.EndsWith(nameof(EditChildRequest.DateOfBirth), StringComparison.OrdinalIgnoreCase))
        {
          patchDbChild.Operations.Add(new Operation<DbChild>(
            item.op, item.path, item.from, DateTime.Parse(item.value.ToString()).Date));
          
          continue;
        }
        
        patchDbChild.Operations.Add(new Operation<DbChild>(item.op, item.path, item.from, item.value));
      }

      return patchDbChild;
    }
  }
}