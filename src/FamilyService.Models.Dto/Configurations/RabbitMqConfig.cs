using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Department;

namespace LT.DigitalOffice.FamilyService.Models.Dto.Configurations
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    //Department
    
    [AutoInjectRequest(typeof(IGetDepartmentsUsersRequest))]
    public string GetDepartmentsUsersEndpoint { get; set; }
  }
}
