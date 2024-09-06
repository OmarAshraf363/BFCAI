using Banha_UniverCity.Models;
using Banha_UniverCity.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Banha_UniverCity.Areas.Admin
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public DepartmentController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var listOfDepartments = _unitOfWork.departmentRepository.Get(null,e=>e.Courses);
            return View(listOfDepartments);
        }
        public IActionResult Details(int id)
        {
         var department=_unitOfWork.departmentRepository.GetOne(e=>e.DepartmentID==id,e=>e.Courses,e=>e.Users,e=>e.Events);   
            return View(department); 
        }

        [HttpGet]
        public IActionResult UpSert(int? id )
        {
            Department department = new();
            if(id != null)
            {
                department= _unitOfWork.departmentRepository.GetOne(e => e.DepartmentID == id);
                return PartialView(department);
            }
            return PartialView(department);
        }
        [HttpPost]
        public IActionResult UpSert(Department department)
        {
            if (ModelState.IsValid)
            {
                if(department.DepartmentID == 0)
                {
                    _unitOfWork.departmentRepository.Create(department);
                    TempData["alert"] = "Added successfully";


                }
                else
                {
                    _unitOfWork.departmentRepository.Edit(department);
                    TempData["alert"] = "Edited successfully";
                }
                var result = StaticData.CheckValidation(ModelState, Request, true);
                if (result != null)
                {
                    return result;
                }
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                var result = StaticData.CheckValidation(ModelState, Request, false);
                if (result!=null)
                {
                    return result;
                }
                return BadRequest();
            }
        }

        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.departmentRepository.GetOne(e => e.DepartmentID == id);
            if (department != null)
            {
                _unitOfWork.departmentRepository.Delete(department);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else { return NotFound(); }
        }
    }
}
