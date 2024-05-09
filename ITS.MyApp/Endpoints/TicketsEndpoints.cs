using Microsoft.AspNetCore.Http.HttpResults;
using ITS.MyApp.Services;
using ITS.MyApp.Web.Models;

namespace ITS.MyApp.Web.Endpoints
{
    public static class TicketsEndpoints
    {
        public static IEndpointRouteBuilder MapTicketsEndpoints(this IEndpointRouteBuilder builder)
        {
            var ticketGroup = builder.MapGroup("/api/v1/tickets")
                                     .WithOpenApi()
                                     .WithTags("Tickets");

            ticketGroup.MapGet("/", GetTicketAsync)
                       .WithName("GetTickets")
                       .WithSummary("Get all tickets")
                       .WithDescription("Return the list of all tickets.");

            ticketGroup.MapGet("/{id:int}", GetTicketByIdAsync)
                       .WithName("GetTicketById");

            ticketGroup.MapPost("/", InsertTicketAsync)
                       .WithName("InsertTicket");

            ticketGroup.MapPut("/{id:int}", UpdateTicketAsync)
                       .WithName("UpdateTicket");

            ticketGroup.MapDelete("/{id:int}", DeleteTicketAsync)
                       .WithName("DeleteTicket");

            return builder;
        }

        private static async Task<Ok<IEnumerable<Ticket>>> GetTicketAsync(TicketService data)
        {
            var list = await data.GetTicketsAsync();
            return TypedResults.Ok(list);
        }

        private static async Task<Results<Ok<Ticket>, NotFound>> GetTicketByIdAsync(int id, TicketService data)
        {
            var ticket = await data.GetTicketAsync(id);
            if (ticket is null)
                return TypedResults.NotFound();

            return TypedResults.Ok(ticket);
        }

        private static async Task<NoContent> InsertTicketAsync(Ticket ticket, TicketService data)
        {
            await data.InsertTicketAsync(ticket);
            return TypedResults.NoContent();
        }

        private static async Task<Results<NoContent, NotFound>> UpdateTicketAsync(int id, Ticket ticket, TicketService data)
        {
            var temp = await data.GetTicketAsync(id);
            if (temp is null) return TypedResults.NotFound();

            ticket.TicketId = id;
            await data.UpdateTicketAsync(ticket);
            return TypedResults.NoContent();
        }

        private static async Task<Results<NoContent, NotFound>> DeleteTicketAsync(int id, TicketService data)
        {
            var temp = await data.GetTicketAsync(id);
            if (temp is null) return TypedResults.NotFound();

            await data.DeleteTicketAsync(id);
            return TypedResults.NoContent();
        }
    }
}
