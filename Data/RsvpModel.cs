using Newtonsoft.Json;

namespace wedding_site.Data;

public class RsvpModel 
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; init; } = string.Empty;
    [JsonProperty(PropertyName = "partition")]
    public string Partition => Id;
    public string ConfirmationCode { get; init; } = string.Empty;
    public string Name { get; set;} = string.Empty;
    public bool Attending { get; set; }
    public string FoodAlergies { get; set; } = string.Empty;
}
