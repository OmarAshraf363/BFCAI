using Banha_UniverCity.Models;
using Banha_UniverCity.Repository.IRepository;
using BFCAI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banha_UniverCity.Areas.Student.Controllers
{
    [Area("Student")]
    public class DashBoardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public DashBoardController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var studentId = _userManager.GetUserId(User);

            // جلب بيانات التسجيل الخاصة بالطالب مباشرة مع الكورسات والعلاقات المرتبطة
            var listOfStudentCourse = _unitOfWork.enrollmentRepository
                .Get(e => e.StudentId == studentId,e=>e.Course).AsQueryable()
                .Include(e => e.Course) // تضمين الكورس
                    .ThenInclude(c => c.CourseCurricula) // تضمين المنهج
                    .ThenInclude(cc => cc.CourseResources) // تضمين الموارد
                .Include(e => e.Course) // تضمين الكورس
                    .ThenInclude(c => c.CourseCurricula) // تضمين المنهج
                    .ThenInclude(cc => cc.CourseVideos) // تضمين الفيديوهات
                .Include(e => e.Course) // تضمين الكورس
                    .ThenInclude(c => c.CourseCurricula) // تضمين المنهج
                    .ThenInclude(cc => cc.Assignments) // تضمين الواجبات
                .Select(e => e.Course) // الحصول على الكورس بعد تضمين العلاقات
                .ToList();


            var coursIds = listOfStudentCourse.Select(e => e.CourseID);



            var courseSchedules =_unitOfWork.classSchedulere.Get(e=>coursIds.Contains(e.CourseId));
         

            // ترتيب مواعيد جدول الحصص الدراسية بناءً على أقرب ميعاد
            var sortedSchedules = courseSchedules
                .OrderBy(s => s.StartTime) // ترتيب حسب تاريخ الجدول
                .ThenBy(s => s.StartTime)  // ثم ترتيب حسب الوقت
                .ToList();

            var listOfNonEnrolledCourse = _unitOfWork.courseRepository.Get(e => !coursIds.Contains(e.CourseID), e => e.Instructor).ToList();



            HomeStudentViewModels model = new HomeStudentViewModels
            {
                Courses = listOfStudentCourse,
                ClassSchedule = sortedSchedules,
                Feedbacks = _unitOfWork.feedbackRepository.Get(e => e.TargetStudentUserId == studentId).ToList(),
                SomeAvailableCourse = listOfNonEnrolledCourse,
                Enrollments=_unitOfWork.enrollmentRepository.Get().ToList()
                
            };

            return View(model);
        }

        public IActionResult CourseDetails(int? id)
        {
            var course=_unitOfWork.courseRepository.GetOne(e=>e.CourseID== id,e=>e.LearningObjectives,expression=>expression.TopicsCovered);
            return PartialView(course);
        }


    }
}
