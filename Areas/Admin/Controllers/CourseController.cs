using Banha_UniverCity.Models;
using Banha_UniverCity.Repository.IRepository;
using Banha_UniverCity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Banha_UniverCity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public CourseController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.mostPopularCourse = _unitOfWork.enrollmentRepository.Get()
                .GroupBy(e => e.CourseID)
                .OrderByDescending(e => e.Count())
                .Select(s => new
                {
                    Course = _unitOfWork.courseRepository.GetOne(e => e.CourseID == s.Key),
                    Count = s.Count()
                });

            return View(_unitOfWork.courseRepository.Get(null, e => e.Instructor, e => e.Department, e => e.Enrollments));

        }

        public IActionResult Details(int id) => View(_unitOfWork.courseRepository.GetOne(e => e.CourseID == id, e => e.Instructor, e => e.Department, e => e.Enrollments));
        [HttpGet]
        public IActionResult UpSert(int? id, bool? fromDepartment)
        {
            Course? course = new();
            ViewBag.Departments = _unitOfWork.departmentRepository.Get().Select(e => new SelectListItem
            {
                Text = e.DepartmentName,
                Value = e.DepartmentID.ToString(),
            });
            var users = _userManager.Users.ToList();
            var listOfUsers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                listOfUsers.Add(user as ApplicationUser);
            }
            ViewBag.Instructors = listOfUsers.Where(e => e.UserType == StaticData.role_Instructor)
                .Select(e => new SelectListItem()
                {
                    Text = e.UserName,
                    Value = e.Id
                });
            if (id != null)
            {
                course = _unitOfWork.courseRepository.GetOne(e => e.CourseID == id);
                return View(course);
            }
            if (fromDepartment == true)
            {
                ViewBag.fromDepartment = fromDepartment.ToString();
            }

            return course != null ? View(course) : NotFound();

        }
        [HttpPost]
        public IActionResult UpSert(Course course, bool? fromDepartment)
        {
            if (ModelState.IsValid)
            {
                if (course.CourseID == 0)
                {
                    _unitOfWork.courseRepository.Create(course);
                    TempData["alert"] = "Added successfully";
                }
                else
                {
                    _unitOfWork.courseRepository.Edit(course);
                    TempData["alert"] = "Edited successfully";
                }
                _unitOfWork.Commit();

                // Redirect based on department context
                if (fromDepartment.HasValue && fromDepartment.Value)
                {
                    TempData["alert"] = "Added successfully";
                    return RedirectToAction("Details", "Department", new { id = course.DepartmentId });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(course);
            }
        }

        public IActionResult Delete(int id)
        {
            var course = _unitOfWork.courseRepository.GetOne(e => e.CourseID == id);
            if (course != null)
            {
                _unitOfWork.courseRepository.Delete(course);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else { return NotFound(); }
        }
    }
}
