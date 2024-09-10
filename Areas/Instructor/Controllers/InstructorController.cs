using Banha_UniverCity.Models;
using Banha_UniverCity.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banha_UniverCity.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public InstructorController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Dashboard()
        {

            // Get the instructor's ID from the logged-in user
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Fetch data related to the instructor
            var totalCourses = _unitOfWork.courseRepository.Get(c => c.InstructorId == instructorId).Count();
            var totalExams = _unitOfWork.examRepository.Get(e => e.InstructorId == instructorId).Count();
            var totalQuestions = _unitOfWork.qusetionRepository.Get(q => q.Exam.InstructorId == instructorId).Count();
            var totalStudents = _unitOfWork.enrollmentRepository.Get(e => e.Course.InstructorId == instructorId).Select(e => e.StudentId).Distinct().Count();

            // Pass data to the view model
            var model = new BFCAI.Models.ViewModels.InstructorDashboardViewModel
            {
                TotalCourses = totalCourses,
                TotalExams = totalExams,
                TotalQuestions = totalQuestions,
                TotalStudents = totalStudents
            };

            return View(model);
        }

        public IActionResult Courses()
        {
            var listOfCourses = _unitOfWork.courseRepository.Get(e => e.InstructorId == _userManager.GetUserId(User),e=>e.CourseCurricula);
            return View(listOfCourses);
        }

        [HttpGet]
        public IActionResult UpsertCourseCurriculum(int? id, int courseId)
        {
            CourseCurriculum model = new CourseCurriculum
            {
                CourseID = courseId
            };

            if (id.HasValue)
            {
                model = _unitOfWork.curriculumRepository.GetOne(e=>e.CourseCurriculumID==id.Value);
                if (model == null)
                {
                    return NotFound();
                }
            }

            return PartialView("_UpsertCourseCurriculum", model);
        }

        [HttpPost]
        public IActionResult UpsertCourseCurriculum(CourseCurriculum model)
        {
            if (ModelState.IsValid)
            {
                if (model.CourseCurriculumID == 0)
                {
                    
                    // إضافة جديد
                    _unitOfWork.curriculumRepository.Create(model);
                }
                else
                {
                    // تعديل المنهج الحالي
                    _unitOfWork.curriculumRepository.Edit(model);
                }

                _unitOfWork.Commit();
                return RedirectToAction("Details", "Course", new { id = model.CourseID });
            }

            return PartialView("_UpsertCourseCurriculum", model);
        }

        public IActionResult GetCourseCurriculum(int courseId)
        {
            var course = _unitOfWork.courseRepository.Get(
                e => e.CourseID == courseId,
                e => e.CourseCurricula
            ).FirstOrDefault();

            if (course == null)
            {
                return NotFound();
            }

            return PartialView("_CourseCurriculumPartial", course);
        }



    }
}
