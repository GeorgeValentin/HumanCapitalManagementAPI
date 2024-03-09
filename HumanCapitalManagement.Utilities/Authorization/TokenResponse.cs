using Newtonsoft.Json;

namespace HumanCapitalManagement.Utilities.Authorization;

public class TokenResponse
{
    [JsonProperty(PropertyName = "access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty(PropertyName = "expires_in")]
    public string? ExpiresIn { get; set; }

    [JsonProperty(PropertyName = "token_type")]
    public string? TokenType { get; set; }

}
