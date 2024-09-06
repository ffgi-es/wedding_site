using Newtonsoft.Json;

namespace wedding_site.Data;

public class Entity<T> {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; init; } = string.Empty;
    [JsonProperty(PropertyName = "partition")]
    public string Partition => Id;

    public T? Payload { get; init; }
}
