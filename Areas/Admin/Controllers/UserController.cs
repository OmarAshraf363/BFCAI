using Banha_UniverCity.Models;
using Banha_UniverCity.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Banha_UniverCity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string filter)
        {
            var applicationUsers=new List<ApplicationUser>();
            var usersFromIdentity=_userManager.Users.ToList();
            foreach (var user in usersFromIdentity)
            {
                applicationUsers.Add(user as ApplicationUser);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                applicationUsers=applicationUsers.Where(e => e.UserType == filter).ToList();
            }
            return View(applicationUsers);
        }


        public async Task<IActionResult> LockUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Lock out the user until a specific date
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UnlockUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Unlock the user
            await _userManager.SetLockoutEndDateAsync(user, null);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
