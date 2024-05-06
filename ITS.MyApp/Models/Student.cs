namespace ITS.MyApp.Web.Models;

public class Student

{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string BirthPlace { get; set; }
    public DateOnly BirthDate { get; set; }
    public string FiscalCode { get; set; }
    public string Email { get; set; }
    public string Iban { get; set; }
    public string CourseName { get; set; }

}
