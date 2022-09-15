using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LT.DigitalOffice.FamilyService.Models.Db;
using LT.DigitalOffice.FamilyService.Data.Provider;
using LT.DigitalOffice.FamilyService.Data.Interfaces;

namespace LT.DigitalOffice.FamilyService.Data
{
  public class ChildRepository : IChildRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

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
  }
}
