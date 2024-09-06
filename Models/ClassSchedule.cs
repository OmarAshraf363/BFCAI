namespace Banha_UniverCity.Models
{
    public class ClassSchedule
    {
        public int ClassScheduleId { get; set; } 
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; } 
        public string DayOfWeek { get; set; } = string.Empty; // The day of the week for the class (e.g., "Monday")

        // Foreign key to the Course
        public int CourseId { get; set; } // Foreign key referencing the Course
        public Course Course { get; set; } = default!; // Navigation property to the Co
    }
}
