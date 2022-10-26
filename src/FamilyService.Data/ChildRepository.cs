using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Data.Provider;
using LT.DigitalOffice.FamilyService.Data.Interfaces;
using LT.DigitalOffice.FamilyService.Models.Dto.Requests.Child.Filters;

namespace LT.DigitalOffice.FamilyService.Data
{
  public class ChildRepository : IChildRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private IQueryable<DbChild> CreateFindPredicates(
      FindChildrenFilter filter,
      IQueryable<DbChild> dbChildren,
      List<Guid> departmentsUsers)
    {
      if (departmentsUsers is not null)
      {
        dbChildren = dbChildren.Where(dbChild => departmentsUsers.Contains(dbChild.ParentUserId));
      }

      dbChildren = dbChildren.Where(c => c.IsActive);
      
      if (filter.ParentUserId.HasValue)
      {
        dbChildren = dbChildren.Where(ch => ch.ParentUserId == filter.ParentUserId);
      }

      if (filter.LowerAgeLimit.HasValue)
      {
        dbChildren = dbChildren.Where(ch => ch.DateOfBirth >= filter.LowerAgeLimit);
      }

      if (filter.UpperAgeLimit.HasValue)
      {
        dbChildren = dbChildren.Where(ch => ch.DateOfBirth <= filter.UpperAgeLimit);
      }

      if (filter.Gender.HasValue)
      {
        dbChildren = dbChildren.Where(ch => ch.Gender == (int)filter.Gender);
      }

      return dbChildren;
    }

    public ChildRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid?> CreateAsync(DbChild dbChild)
    {
      if (dbChild is null)
      {
        return null;
      }

      _provider.Children.Add(dbChild);
      await _provider.SaveAsync();

      return dbChild.Id;
    }

    public Task<bool> DoesValueExist(Guid ParentUserId, string Name, DateTime DateOfBirth)
    {
      return _provider.Children
        .AnyAsync(c => c.ParentUserId == ParentUserId
          && c.Name == Name && c.DateOfBirth == DateOfBirth);
    }

    public async Task<(List<DbChild> dbChildren, int totalCount)> FindAsync(FindChildrenFilter filter, List<Guid> departmentsUsers)
    {
      if (filter is null
        || (filter.Department is not null 
          && !departmentsUsers.Any()))
      {
        return default;
      }

      IQueryable<DbChild> childrenQuery = CreateFindPredicates(
        filter,
        _provider.Children.AsQueryable(),
        departmentsUsers);

       return (
        await childrenQuery.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await childrenQuery.CountAsync());
    }
  }
}
