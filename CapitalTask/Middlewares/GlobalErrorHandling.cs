using CapitalTask.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace CapitalTask.Middlewares;

public class GlobalErrorHandling(ILogger<GlobalErrorHandling> logger,
    IWebHostEnvironment hostEnvironment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception Occured: {exception}", exception.Message);


        var apiResponse = ApiResponse.Failure(StatusCodes.Status500InternalServerError,
            "Operation failed, please try again");

        if (hostEnvironment.IsDevelopment())
            apiResponse.Messages = [$"{exception.Message}\n{exception.InnerException}"];
        else
            apiResponse.Messages = ["Operation failed, please try again"];

        await httpContext.Response
            .WriteAsJsonAsync(apiResponse, cancellationToken: cancellationToken);

        return true;
    }
}