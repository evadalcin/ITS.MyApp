namespace ITS.MyApp.Web.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int DeviceId { get; set;}
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public string Ticketstate { get; set; }

        public string DeviceState { get; set;}
       
        public string Description { get; set; }

        public string Title { get; set; }
    }
}
