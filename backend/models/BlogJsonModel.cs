namespace backend.models;

using System.Text.Json.Serialization;

public class BlogJsonModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}
