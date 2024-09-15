using Banha_UniverCity.Models;
using Banha_UniverCity.Repository.IRepository;
using Banha_UniverCity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;

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
        //[HttpGet]
        //public IActionResult UpSert(int? id, bool? fromDepartment,int? departmentId)
        //{
        //    Course? course = new();
        //    ViewBag.Departments = _unitOfWork.departmentRepository.Get().Select(e => new SelectListItem
        //    {
        //        Text = e.DepartmentName,
        //        Value = e.DepartmentID.ToString(),
        //    });
        //    var users = _userManager.Users.ToList();
        //    var listOfUsers = new List<ApplicationUser>();
        //    foreach (var user in users)
        //    {
        //        listOfUsers.Add(user as ApplicationUser);
        //    }
        //    ViewBag.Instructors = listOfUsers.Where(e => e.UserType == StaticData.role_Instructor)
        //        .Select(e => new SelectListItem()
        //        {
        //            Text = e.UserName,
        //            Value = e.Id
        //        });
        //    if (id != null)
        //    {
        //        course = _unitOfWork.courseRepository.GetOne(e => e.CourseID == id);
        //        return View(course);
        //    }
        //    if (fromDepartment == true)
        //    {
        //        ViewBag.fromDepartment = fromDepartment.ToString();
        //        ViewBag.drpartmentId = departmentId;
        //        ViewBag.departmentName = _unitOfWork.departmentRepository.GetOne(e => e.DepartmentID == departmentId)?.DepartmentName;
        //    }

        //    return course != null ? View(course) : NotFound();

        //}
        //[HttpPost]
        //public IActionResult UpSert(Course course, bool? fromDepartment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        List<TopicCovered> topicsCovered = new List<TopicCovered>();
        //        foreach (var topic in course.TopicsCovered)
        //        {
        //            topic.CourseID = course.CourseID;
        //            topicsCovered.Add(topic);
        //        }
        //        List<LearningObjective> learningObjectives = new List<LearningObjective>();
        //        foreach (var item in course.LearningObjectives)
        //        {
        //            item.CourseID = course.CourseID;
        //            learningObjectives.Add(item);
        //        }

        //        _unitOfWork.learningObjectiveRepository.AddRange(learningObjectives);
        //        _unitOfWork.topicCoveresRepository.AddRange(topicsCovered);
        //        _unitOfWork.Commit();
        //        if (course.CourseID == 0)
        //        {

        //            _unitOfWork.courseRepository.Create(course);
        //            TempData["alert"] = "Added successfully";
        //        }
        //        else
        //        {
        //            _unitOfWork.courseRepository.Edit(course);
        //            TempData["alert"] = "Edited successfully";
        //        }
        //        _unitOfWork.Commit();

        //        // Redirect based on department context
        //        if (fromDepartment.HasValue && fromDepartment.Value)
        //        {
        //            TempData["alert"] = "Added successfully";
        //            return RedirectToAction("Details", "Department", new { id = course.DepartmentId });
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(course);
        //    }
        //}


        [HttpGet]
        public IActionResult UpSert(int? id)
        {

            var listOfUsers = StaticData.GetUsers(_userManager);

            CourseVM model = new()
            {
                Departments = _unitOfWork.departmentRepository.Get().ToList(),
                Users = listOfUsers.Where(e => e.UserType == StaticData.role_Instructor).ToList(),
            };
            if (id == null || id == 0)
            {
                return View(model);
            }
            var course = _unitOfWork.courseRepository.GetOne(e => e.CourseID == id);
            if (course == null) { return NotFound(); }
            model.CourseID = course.CourseID;
            model.CourseName = course.CourseName;
            model.Credits = course.Credits;
            model.DepartmentId = course.DepartmentId;
            model.Description = course.Description;
            model.EndDate = course.EndDate;
            model.StartDate = course.StartDate;
            model.Price = course.Price;
            model.InstructorId = course.InstructorId;
            model.DemoVideoUrl = course.DemoVideoUrl;
            model.TopicCovereds = course.TopicsCovered;
            model.LearningObjectives = course.LearningObjectives;
            model.TopicCovereds = _unitOfWork.topicCoveresRepository.Get(e => e.CourseID == id).ToList();
            model.LearningObjectives = _unitOfWork.learningObjectiveRepository.Get(e => e.CourseID == id).ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult UpSert(CourseVM model)
        {
            if (ModelState.IsValid)
            {
                Course course;
                if (model.CourseID == 0)
                {
                    course = new()
                    {
                        CourseName = model.CourseName,
                        Credits = model.Credits,
                        DepartmentId = model.DepartmentId,
                        Description = model.Description,
                        EndDate = model.EndDate,
                        StartDate = model.StartDate,
                        Price = model.Price,
                        InstructorId = model.InstructorId,
                        DemoVideoUrl = model.DemoVideoUrl,
                        NumOfEnrollments = model.NumOfEnrollments,
                        

                    };
                    _unitOfWork.courseRepository.Create(course);
                    _unitOfWork.Commit();


                    //set topic
                    _unitOfWork.topicCoveresRepository.AddCourseTopicsInTopicTable(model.TopicCovereds,course.CourseID);
                    

                    //set obJectives
                   _unitOfWork.learningObjectiveRepository.AddCourseOpjectivesInOpjectiveTable(model.LearningObjectives,course.CourseID);
                   
                    
                    _unitOfWork.Commit();

                    TempData["alert"] = "Added successfully";
                }
                else
                {
                    course=_unitOfWork.courseRepository.GetOne(e=>e.CourseID==model.CourseID);
                    if (course == null) {return NotFound();}


                    course.CourseName = model.CourseName;
                    course.Credits = model.Credits;
                    course.DepartmentId = model.DepartmentId;
                    course.Description = model.Description;
                    course.EndDate = model.EndDate;
                    course.StartDate = model.StartDate;
                    course.Price = model.Price;
                    course.InstructorId = model.InstructorId;
                    course.DemoVideoUrl = model.DemoVideoUrl;
                    course.NumOfEnrollments = model.NumOfEnrollments;

                    _unitOfWork.courseRepository.Edit(course); ;
                    _unitOfWork.Commit();


                    //remove Olds
                    var oldTopics =_unitOfWork.topicCoveresRepository.Get(e=>e.CourseID==course.CourseID);
                    _unitOfWork.topicCoveresRepository.DeleteRange(oldTopics);
                   

                    var oldOpjectives = _unitOfWork.learningObjectiveRepository.Get(e => e.CourseID == course.CourseID);
                    _unitOfWork.learningObjectiveRepository.DeleteRange(oldOpjectives);
                    _unitOfWork.Commit();

                    //set topic
                    _unitOfWork.topicCoveresRepository.AddCourseTopicsInTopicTable(model.TopicCovereds, course.CourseID);


                    //set obJectives
                    _unitOfWork.learningObjectiveRepository.AddCourseOpjectivesInOpjectiveTable(model.LearningObjectives, course.CourseID);


                    _unitOfWork.Commit();
                    TempData["alert"] = "Edited successfully";

                }
                return RedirectToAction("Index");

            }
            else
            {
                model.Departments = _unitOfWork.departmentRepository.Get().ToList();
                var listOfUsers = StaticData.GetUsers(_userManager);
                model.Users = listOfUsers.Where(e => e.UserType == StaticData.role_Instructor);
                return View(model);
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
