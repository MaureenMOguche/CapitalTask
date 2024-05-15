using FluentValidation;
#pragma warning disable

namespace CapitalTask.Dtos;

public class ProgramApplicationDtoValidator : AbstractValidator<ProgramApplicationDto>
{
    public ProgramApplicationDtoValidator()
    {
        RuleFor(x => x.ProgramId).NotEmpty().WithMessage("ProgramId is required");
    }
}
