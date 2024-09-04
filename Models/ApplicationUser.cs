using Microsoft.AspNetCore.Identity;

namespace Banha_UniverCity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; } = string.Empty; // الاسم الكامل للمستخدم
        public string UserType { get; set; } = string.Empty; // نوع المستخدم: "Student" أو "Instructor"
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();  // الارتباطات بالكورسات (للطلاب)
        public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();// الكورسات التي يقوم بتدريسها (للمعلمين)
    }
}
