using MySql.Data.MySqlClient;
using Dapper;
using ITS.MyApp.Web.Models;

namespace ITS.MyApp.Services
{
    public class TicketService
    {
        private readonly string _connectionString;

        public TicketService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
                SELECT
                    TicketId,
                    DeviceId,
                    StartTime,
                    EndTime,
                    Ticketstate,
                    DeviceState,
                    Description,
                    Title
                FROM tickets;";

            return await connection.QueryAsync<Ticket>(query);
        }

        public async Task<Ticket?> GetTicketAsync(int ticketId)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
                SELECT
                    TicketId,
                    DeviceId,
                    StartTime,
                    EndTime,
                    Ticketstate,
                    DeviceState,
                    Description,
                    Title
                FROM tickets
                WHERE TicketId = @ticketId;";

            return await connection.QueryFirstOrDefaultAsync<Ticket>(query, new { ticketId });
        }

        public async Task InsertTicketAsync(Ticket ticket)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
                INSERT INTO tickets (DeviceId, StartTime, EndTime, Ticketstate, DeviceState, Description, Title)
                VALUES (@DeviceId, @StartTime, @EndTime, @Ticketstate, @DeviceState, @Description, @Title);";

            await connection.ExecuteAsync(query, ticket);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
                UPDATE tickets
                SET
                    DeviceId = @DeviceId,
                    StartTime = @StartTime,
                    EndTime = @EndTime,
                    Ticketstate = @Ticketstate,
                    DeviceState = @DeviceState,
                    Description = @Description,
                    Title = @Title
                WHERE TicketId = @TicketId;";

            await connection.ExecuteAsync(query, ticket);
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = "DELETE FROM tickets WHERE TicketId = @ticketId;";

            await connection.ExecuteAsync(query, new { ticketId });
        }
    }
}
