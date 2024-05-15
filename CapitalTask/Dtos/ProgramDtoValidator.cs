using FluentValidation;

namespace CapitalTask.Dtos;

public class ProgramDtoValidator : AbstractValidator<CreateProgramDto>
{
    public ProgramDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Program Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleForEach(x => x.Questions).SetValidator(new QuestionDtoValidator());
    }
}


public class UpdateProgramDtoValidator : AbstractValidator<UpdateProgramDto>
{
    public UpdateProgramDtoValidator()
    {
        RuleForEach(x => x.Questions).SetValidator(new QuestionDtoValidator());
    }
}

