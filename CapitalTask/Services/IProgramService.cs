#pragma warning disable
using CapitalTask.Dtos;
using CapitalTask.Response;

namespace CapitalTask.Services;

public interface IProgramService
{
    Task<ApiResponse> CreateProgram(CreateProgramDto program);
    Task<ApiResponse> GetProgram(string programId);
    Task<ApiResponse> GetPrograms(BaseQueryParams queryParams);
    Task<ApiResponse> UpdateQuestions(string programId, UpdateProgramDto updateProgram);
    Task<ApiResponse> DeleteProgram(string programId);
    Task<ApiResponse> ApplyForProgram(ProgramApplicationDto programApplication);
    Task<ApiResponse> GetAllApplications();
}

