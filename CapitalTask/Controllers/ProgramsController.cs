using CapitalTask.Dtos;
using CapitalTask.Entities;
using CapitalTask.Response;
using CapitalTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers;

/// <summary>
/// Contoller for managing programs
/// </summary>
/// <param name="programService"></param>
[Route("api/[controller]")]
[ApiController]
public class ProgramsController(IProgramService programService) : ControllerBase
{
    /// <summary>
    /// Creates a new program
    /// </summary>
    /// <param name="programDto"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    [HttpPost("create-program")]
    public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto programDto)
    {
        var response = await programService.CreateProgram(programDto);
        return StatusCode(response.StatusCode, response);
    }


    /// <summary>
    /// Gets all programs
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    [HttpGet("get-programs")]
    public async Task<IActionResult> GetPrograms()
    {
        var response = await programService.GetPrograms();
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Gets a single program by it's id
    /// </summary>
    /// <param name="programId"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    [HttpGet("get-program/{programId}")]
    public async Task<IActionResult> GetProgram(string programId)
    {
        var response = await programService.GetProgram(programId);
        return StatusCode(response.StatusCode, response);
    }


    /// <summary>
    /// Update questions for a program
    /// </summary>
    /// <param name="programId"></param>
    /// <param name="updateProgram"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    [HttpPut("update-program-questions/{programId}")]
    public async Task<IActionResult> UpdateProgramQuestions(string programId, [FromBody] UpdateProgramDto updateProgram)
    {
        var response = await programService.UpdateQuestions(programId, updateProgram);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Deletes a program
    /// </summary>
    /// <param name="programId"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    [HttpDelete("delete-program/{programId}")]
    public async Task<IActionResult> DeleteProgram(string programId)
    {
        var response = await programService.DeleteProgram(programId);
        return StatusCode(response.StatusCode, response);
    }


    /// <summary>
    /// Apply for a program
    /// </summary>
    /// <param name="programApplication"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    [HttpPost("apply-for-program/{programId}")]
    public async Task<IActionResult> ApplyForProgram(ProgramApplication programApplication)
    {
        var response = await programService.ApplyForProgram(programApplication);
        return StatusCode(response.StatusCode, response);
    }
}
