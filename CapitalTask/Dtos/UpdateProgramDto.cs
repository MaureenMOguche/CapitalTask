using System.Text.Json.Serialization;

namespace CapitalTask.Dtos;

public class UpdateProgramDto
{
    [JsonPropertyName("questions")]
    public List<QuestionDto> Questions { get; set; } = [];
}