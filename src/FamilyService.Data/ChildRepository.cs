using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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

    private IQueryable<DbChild> CreateFindPredicates(
      FindChildrenFilter filter,
      IQueryable<DbChild> query,
      List<Guid> departmentsUsers)
    {
      if (departmentsUsers is not null
        && departmentsUsers.Any())
      {
        query = query.Where(dbChild => departmentsUsers.Contains(dbChild.ParentUserId));
      }
      
      if (filter.ParentUserId.HasValue)
      {
        query = query.Where(ch => ch.ParentUserId == filter.ParentUserId);
      }

      if (filter.LowerAgeLimit.HasValue)
      {
        query = query.Where(ch => ch.DateOfBirth <= DateTime.UtcNow.AddYears(-filter.LowerAgeLimit.Value));
      }

      if (filter.UpperAgeLimit.HasValue)
      {
        query = query.Where(ch => ch.DateOfBirth >= DateTime.UtcNow.AddYears(-filter.UpperAgeLimit.Value));
      }

      if (filter.Gender.HasValue)
      {
        query = query.Where(ch => ch.Gender == (int)filter.Gender);
      }

      return query;
    }

    public ChildRepository(
      IDataProvider provider)
    {
      _provider = provider;
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

    public Task<bool> DoesValueExist(Guid parentUserId, string name, DateTime dateOfBirth)
    {
      return _provider.Children
        .AnyAsync(c => c.ParentUserId == parentUserId
          && c.Name == name && c.DateOfBirth == dateOfBirth);
    }

    public async Task<(List<DbChild> dbChildren, int totalCount)> FindAsync(FindChildrenFilter filter, List<Guid> departmentsUsers)
    {
      if (filter is null)
      {
        return default;
      }
      
      if (filter.Departments is not null
        && !departmentsUsers.Any())
      {
        return (new List<DbChild>(), 0);
      }

      IQueryable<DbChild> childrenQuery = CreateFindPredicates(
        filter,
        _provider.Children.AsQueryable().Where(c => c.IsActive),
        departmentsUsers);

       return (
        await childrenQuery.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await childrenQuery.CountAsync());
    }
  }
}
