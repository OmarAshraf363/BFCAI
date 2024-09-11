using Banha_UniverCity.Models;
using Banha_UniverCity.Repository.IRepository;
using BFCAI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Banha_UniverCity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrollmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public EnrollmentController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var courses = _unitOfWork.courseRepository.Get().Select(c => new SelectListItem
            {
                Text = c.CourseName,
                Value = c.CourseID.ToString()
            }).ToList();

            // Get latest enrollments and total count
            var enrollments = _unitOfWork.enrollmentRepository.Get(null,e=>e.Course,e=>e.Student)
                                   .OrderByDescending(e => e.EnrollmentID)
                                   .Take(10) // Get last 10 enrollments
                                   .ToList();

            var totalEnrollments = _unitOfWork.enrollmentRepository.Get().Count();

            var model = new EnrollmentViewModel
            {
                Courses = courses,
                Enrollments = enrollments,
                TotalEnrollments = totalEnrollments
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EnrollByEmail(EnrollmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData["Error"] = "Student with this email does not exist.";
                    return RedirectToAction(nameof(Index));
                }

                var enrollment = new Enrollment
                {
                    CourseID = model.CourseId,
                    StudentId = user.Id
                };

                _unitOfWork.enrollmentRepository.Create(enrollment);
                 _unitOfWork.Commit();

                TempData["Success"] = "Student has been successfully enrolled.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Invalid data provided.";
            return RedirectToAction(nameof(Index));
        }

    }


}
