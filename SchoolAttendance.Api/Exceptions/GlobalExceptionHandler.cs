using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Application.AcademicSessions;
using SchoolAttendance.Application.SchoolClasses;
using SchoolAttendance.Application.Schools;
using SchoolAttendance.Application.Sections;
using SchoolAttendance.Application.Students;

namespace SchoolAttendance.Api.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An exception occurred.");
        if (exception is DuplicateStudentCodeException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Student code already exists.",
                    Detail = exception.Message
                },
                cancellationToken
            );
            return true;
        }
        if (exception is DuplicateSchoolCodeException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "School code already exists.",
                    Detail = exception.Message
                },
                cancellationToken
            );
            return true;
        }
        if (exception is DuplicateBranchCodeException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Branch code already exists.",
                    Detail = exception.Message
                },
                cancellationToken
            );
            return true;
        }
        if (exception is DuplicateAcademicSessionNameException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Acadmic session in branch already exists.",
                    Detail = exception.Message
                },
                cancellationToken
            );
            return true;
        }
        if (exception is DuplicateSchoolClassNameException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "School class in branch already exists.",
                    Detail = exception.Message
                },
                cancellationToken
            );
            return true;
        }
        if (exception is DuplicateSectionNameException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Section in school class already exists.",
                    Detail = exception.Message
                },
                cancellationToken
            );
            return true;
        }
        return false;
    }
}