using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.FamilyService.Models.Db;

namespace LT.DigitalOffice.FamilyService.Data.Interfaces
{
  [AutoInject]
  public interface IChildRepository
  {
    Task<Guid?> CreateAsync(DbChild dbChild);
    Task<bool> DoesValueExist(Guid ParentUserId, string Name, DateTime DateOfBirth);
  }
}
