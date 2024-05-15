using CapitalTask.Data;
using CapitalTask.Dtos;
using CapitalTask.Entities;
using CapitalTask.Response;

namespace CapitalTask.Services;

public class ProgramService(ICosmosDbService cosmosDbService) : IProgramService
{
    public async Task<ApiResponse> ApplyForProgram(ProgramApplication programApplication)
    {
        var programExists = await cosmosDbService.GetItemAsync<QuestionProgram>($"Select * from c where c.id = '{programApplication.ProgramId}' ");

        if (programExists == null)
            return ApiResponse.Failure(StatusCodes.Status404NotFound, "Program not found");

        foreach (var answer in programApplication.Answers)
        {
            if (!programExists.Questions.Any(x => x.QuestionId == answer.QuestionId))
                return ApiResponse.Failure(StatusCodes.Status400BadRequest, "Invalid question id");

            var question = programExists.Questions.FirstOrDefault(x => x.QuestionId == answer.QuestionId);
            if (question.QuestionType == QuestionType.MultipleChoice)
            {
                if (answer.MultipleChoiceAnswers == null || !answer.MultipleChoiceAnswers.Any())
                    return ApiResponse.Failure(StatusCodes.Status400BadRequest, "Please provide answers for multiple choice questions");
            }
            else
            {
                if (string.IsNullOrEmpty(answer.ParagraphAnswer))
                    return ApiResponse.Failure(StatusCodes.Status400BadRequest, "Please provide answers for paragraph questions");
            }

        }

        await cosmosDbService.AddItemAsync(programApplication, programApplication.Id);

        return ApiResponse.Success("Successfully applied for program");
    }

    public async Task<ApiResponse> CreateProgram(CreateProgramDto programDto)
    {
        var programExists = await cosmosDbService.GetItemsAsync<Program>($"Select * from c where c.title = '{programDto.Title}' ");

        if (programExists.Any())
            return ApiResponse.Failure(StatusCodes.Status400BadRequest, "Program already exists");

        List<Question> questionsList = new List<Question>();

        foreach (var question in programDto.Questions)
        {
            switch (question.QuestionType)
            {
                case QuestionType.Paragraph:
                    questionsList.Add(new Question{ QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                case QuestionType.MultipleChoice:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText, MultipleChoice = question.MultipleChoices });
                    break;
                case QuestionType.Dropdown:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText, DropdownChoices = question.DropDownChoices });
                    break;
                case QuestionType.YesNo:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                case QuestionType.Date:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                case QuestionType.Number:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                default:
                    throw new ArgumentException("Invalid question type");
            }

        }

        var program = new QuestionProgram
        {
            Title = programDto.Title,
            Description = programDto.Description,
            Questions = questionsList
        };

        await cosmosDbService.AddItemAsync(program, program.Id);

        return ApiResponse.Success("Successfully created new program");
    }

    public async Task<ApiResponse> DeleteProgram(string id)
    {
        await cosmosDbService.DeleteItemAsync<QuestionProgram>(id);
        return ApiResponse.Success("Successfully deleted program");
    }

    public async Task<ApiResponse> GetProgram(string programId)
    {
        var programExists = await cosmosDbService.GetItemsAsync<QuestionProgram>($"Select * from c where c.id = '{programId}' ");
        if (!programExists.Any())
            return ApiResponse.Failure(StatusCodes.Status404NotFound, "Program not found");

        return ApiResponse<object>.Success(programExists, "Successfully retrieved program");

    }

    public async Task<ApiResponse> GetPrograms()
    {
        var programs = await cosmosDbService.GetItemsAsync<QuestionProgram>("Select * from c");

        return ApiResponse<object>.Success(programs, programs.Any() ? "Successfully retrieved programs" : "There are currently no programs");
    }

    public async Task<ApiResponse> UpdateQuestions(string programId, UpdateProgramDto updateProgram)
    {
        var programExists = await cosmosDbService.GetItemAsync<QuestionProgram>($"Select * from c where c.id = '{programId}' ");
        if (programExists == null)
            return ApiResponse.Failure(StatusCodes.Status404NotFound, "Program not found");

        List<Question> questionsList = new List<Question>();

        foreach (var question in updateProgram.Questions)
        {
            switch (question.QuestionType)
            {
                case QuestionType.Paragraph:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                case QuestionType.MultipleChoice:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText, MultipleChoice = question.MultipleChoices });
                    break;
                case QuestionType.Dropdown:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText, DropdownChoices = question.DropDownChoices });
                    break;
                case QuestionType.YesNo:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                case QuestionType.Date:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                case QuestionType.Number:
                    questionsList.Add(new Question { QuestionType = question.QuestionType, QuestionText = question.QuestionText });
                    break;
                default:
                    throw new ArgumentException("Invalid question type");
            }
        }

        programExists.Questions = questionsList;

        await cosmosDbService.UpdateItemAsync(programId, programExists);

        return ApiResponse.Success("Successfully updated program");
    }

}

