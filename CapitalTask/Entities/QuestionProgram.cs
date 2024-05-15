#pragma warning disable
using Newtonsoft.Json;

namespace CapitalTask.Entities;

public class QuestionProgram
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
    [JsonProperty("questions")]
    public List<Question> Questions { get; set; } = new List<Question>();

    [JsonProperty("dateCreated")]
    public DateTime DateCreated { get; set; } = DateTime.Now;
}
