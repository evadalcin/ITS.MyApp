namespace ITS.MyApp.Services;

using MySql.Data.MySqlClient;
using Dapper;
using ITS.MyApp.Web.Models;

public class StudentService
{

    private readonly string _connectionString;

    public StudentService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string query = """
                    SELECT
                        id,
                        firstname,
                        lastname,
                        birthplace,
                        birthdate,
                        fiscalcode,
                        email,
                        iban,
                        coursename
                    FROM students;
                    """
        ;

        return await connection.QueryAsync<Student>(query);
    }

    public async Task<Student?> GetStudentAsync(int studentId)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string query = """
                    SELECT
                        id,
                        firstname,
                        lastname,
                        birthplace,
                        birthdate,
                        fiscalcode,
                        email,
                        iban,
                        coursename
                    FROM students
                    WHERE id = @id;
                    """
        ;

        return await connection.QueryFirstOrDefaultAsync<Student>(
                                    query,
        new { id = studentId });
    }

    public async Task InsertStudentAsync(Student student)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string query = """
            INSERT INTO students ( firstname, lastname, birthplace, birthdate, fiscalcode, email, iban, coursename)
            VALUES (@firstname, @lastname, @birthplace, @birthdate, @fiscalcode, @email, @iban, @coursename)
            """;

        await connection.ExecuteAsync(query, student);
    }

    public async Task UpdateStudentAsync(Student student)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string query = """
            UPDATE students
            SET
                id = @Id,
                firstname = @firstname,
                lastname = @lastname,
                birthplace = @birthplace,
                birthdate = @birthdate,
                fiscalcode = @fiscalcode,
                email = @email,
                iban = @iban,
                coursename = @coursename
            WHERE id = @id;
            """;

        await connection.ExecuteAsync(query, student);
    }

    public async Task DeleteStudentAsync(int studentId)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string query = "DELETE FROM students WHERE id = @id";

        await connection.ExecuteAsync(query, new { id = studentId });

    }

}