using System;
using Microsoft.AspNetCore.Http;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child;

namespace LT.DigitalOffice.FamilyService.Mappers.Db
{
  public class DbChildMapper : IDbChildMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbChildMapper(
      IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbChild Map(CreateChildRequest request)
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
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow
      };
    }
  }
}
