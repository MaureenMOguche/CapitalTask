using CapitalTask.Entities;
using Newtonsoft.Json;
#pragma warning disable
namespace CapitalTask.Dtos;

public class ProgramApplicationDto
{
    [JsonProperty("userId")]
    public string UserId { get; set; }
    [JsonProperty("programId")]
    public string ProgramId { get; set; }
    public List<QuestionAnswers> Answers { get; set; } = new List<QuestionAnswers>();

    internal ProgramApplication ToProgramApplication()
    {
        return new ProgramApplication
        {
            Answers = Answers,
            ProgramId = ProgramId,
            UserId = UserId,
        };
    }
}
