using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LT.DigitalOffice.FamilyService.Models.Dto.Enums
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum Gender
  {
    Female,
    Male
  }
}
