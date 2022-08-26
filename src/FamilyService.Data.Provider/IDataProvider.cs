using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.EFSupport.Provider;

namespace LT.DigitalOffice.FamilyService.Data.Provider
{
    [AutoInject(InjectType.Scoped)]
    public interface IDataProvider : IBaseDataProvider
    {
        //DbSet<DbMembers> Members{get; set;}
    }
}