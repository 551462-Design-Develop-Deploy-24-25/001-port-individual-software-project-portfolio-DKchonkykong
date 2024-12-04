namespace DDD;

public class StudentSystems
{
    // Self Report class to store student's feedback
    
    public class SelfReport
    {
        public string StudentEmail { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public string Feeling { get; set; }
        public Dictionary<string, int> CourseRatings { get; set; } = new Dictionary<string, int>();
    }

// Booking class to store room booking details
    public class Booking
    {
        public string StudentEmail { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string RoomNumber { get; set; }
        public string SupervisorName { get; set; }
    }
}