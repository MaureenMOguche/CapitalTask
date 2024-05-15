using CapitalTask.Entities;
using FluentValidation;

namespace CapitalTask.Dtos;

public class QuestionDtoValidator : AbstractValidator<QuestionDto>
{
    public QuestionDtoValidator()
    {
        RuleFor(x => x.QuestionType).IsInEnum().WithMessage("Invalid Question Type");

        RuleFor(x => x.QuestionText).NotEmpty().WithMessage("Question Text is required");

        When(x => x.QuestionType == QuestionType.MultipleChoice, () =>
        {
            RuleFor(x => x.MultipleChoices).NotEmpty().WithMessage("Please enter a list of choices");
        });
        
        
        When(x => x.QuestionType == QuestionType.Dropdown, () =>
        {
            RuleFor(x => x.DropDownChoices).NotEmpty().WithMessage("Please enter a list of dropdown choices");
        });

       
    }
}