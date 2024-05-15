#pragma warning disable
namespace CapitalTask.Entities;

public class Question
{
    public string QuestionId { get; set; } = Guid.NewGuid().ToString();
    public QuestionType QuestionType { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public Dictionary<char, string>? MultipleChoice { get; set; }
    public List<string>? DropdownChoices { get; set; }
}

