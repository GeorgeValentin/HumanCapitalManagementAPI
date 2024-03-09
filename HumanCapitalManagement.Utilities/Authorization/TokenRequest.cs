using Newtonsoft.Json;

namespace HumanCapitalManagement.Utilities.Authorization;

public class TokenRequest
{
    [JsonProperty(PropertyName = "client_id")]
    public string ClientId { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "client_secret")]
    public string ClientSecret { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "grant_type")]
    public string GrantType { get; set; } = string.Empty;
}
