using System;

using Microsoft.AspNetCore.Http.HttpResults;
using ITS.MyApp.Services;
using ITS.MyApp.Web.Models;

namespace ITS.MyApp.Web.Endpoints;
public static class StudentsEndpoints
{

    public static IEndpointRouteBuilder MapStudentsEndpoints(
                                            this IEndpointRouteBuilder builder)
    {
        var customerGroup = builder.MapGroup("/api/v1/students")
                                   .WithOpenApi()
                                   .WithTags("Students");

        customerGroup.MapGet("/", GetStudentAsync)
                     .WithName("GetStudents")
                     .WithSummary("Get all students")
                     .WithDescription("Return the list of all students.");

        customerGroup.MapGet("/{id:int}", GetStudentsByIdAsync)
                     .WithName("GetStudentsById");

        customerGroup.MapPost("/", InsertStudentAsync)
                     .WithName("InsertStudent");

        customerGroup.MapPut("/{id:int}", UpdateStudentAsync)
                     .WithName("UpdateStudent");

        customerGroup.MapDelete("/{id:int}", DeleteStudentAsync)
                     .WithName("DeleteStudent");

        return builder;
    }

    private static async Task<Ok<IEnumerable<Student>>> GetStudentAsync(StudentService data)
    {
        var list = await data.GetStudentsAsync();
        return TypedResults.Ok(list);
    }

    private static async Task<Results<Ok<Student>, NotFound>> GetStudentsByIdAsync(int id, StudentService data)
    {
        var student = await data.GetStudentAsync(id);
        if (student is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(student);
    }

    private static async Task<NoContent> InsertStudentAsync(Student student, StudentService data)
    {
        await data.InsertStudentAsync(student);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateStudentAsync(int id, Student student, StudentService data)
    {
        var temp = await data.GetStudentAsync(id);
        if (temp is null) return TypedResults.NotFound();

        student.Id = id;
        await data.UpdateStudentAsync(student);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteStudentAsync(int id, StudentService data)
    {
        var temp = await data.GetStudentAsync(id);
        if (temp is null) return TypedResults.NotFound();

        await data.DeleteStudentAsync(id);
        return TypedResults.NoContent();
    }
}