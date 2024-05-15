#pragma warning disable
using System.Text.Json.Serialization;

namespace CapitalTask.Dtos;

public class CreateProgramDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("questions")]
    public List<QuestionDto> Questions { get; set; } = [];
}

