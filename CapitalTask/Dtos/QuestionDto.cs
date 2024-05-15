using CapitalTask.Entities;
using Newtonsoft.Json;

#pragma warning disable

namespace CapitalTask.Dtos;

public class QuestionDto
{
    [JsonProperty("questionType")]
    public QuestionType QuestionType { get; set; }
    [JsonProperty("questionText")]
    public string QuestionText { get; set; } = string.Empty;

    [JsonProperty("multiplechoice")]
    public Dictionary<char, string>? MultipleChoices { get; set; }

    [JsonProperty("dropdown")]
    public List<string>? DropDownChoices { get; set; }
}
