using System;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests;
using LT.DigitalOffice.FamilyService.Mappers.Db.Interfaces;

namespace LT.DigitalOffice.FamilyService.Mappers.Db
{
  public class DbChildMapper : IDbChildMapper
  {
    public DbChild Map(CreateChildRequest request, Guid creatorId)
    {
      if (request is null)
      {
        return null;
      }

      return new DbChild()
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Gender = (int)request.Gender,
        DateOfBirth = request.DateOfBirth.Date,
        Info = request.Info,
        ParentUserId = request.ParentUserId,
        IsActive = true,
        CreatedBy = creatorId,
        CreatedAtUtc = DateTime.UtcNow
      };
    }
  }
}
