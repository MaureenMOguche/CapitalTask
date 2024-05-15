using Newtonsoft.Json;

namespace CapitalTask.Entities;

public class ProgramApplication
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonProperty("userId")]
    public string UserId { get; set; }
    [JsonProperty("programId")]
    public string ProgramId { get; set; }
    public List<QuestionAnswers> Answers { get; set; } = new List<QuestionAnswers>();
}


public class QuestionAnswers
{
    public string QuestionId { get; set; } = string.Empty;
    public string ParagraphAnswer { get; set; } = string.Empty;
    public List<string>? MultipleChoiceAnswers { get; set; }
}