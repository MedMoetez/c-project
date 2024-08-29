using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppelsDOffresApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppelsDOffresApp.Models;

namespace AppelsDOffresApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Corrected RoleManager to use IdentityRole instead of Utilisateur
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Utilisateur> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<Utilisateur> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
            return RedirectToAction("ManageRoles");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return NotFound();

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in usersInRole)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return RedirectToAction("ManageRoles");

            return View();
        }


        public async Task<IActionResult> ManageUserRoles()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                model.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = userRoles
                });
            }

            return View(model);
        }

        public async Task<IActionResult> ManageUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleName = role.Name,
                    Selected = await _userManager.IsInRoleAsync(user, role.Name)
                };
                model.Add(userRolesViewModel);
            }

            ViewBag.userId = userId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRole(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!result.Succeeded) return View();

            result = await _userManager.AddToRolesAsync(user, roles);
            if (!result.Succeeded) return View();

            return RedirectToAction("ManageUserRoles");
        }


        public IActionResult ManageUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ManageUsers");
                }
                // Handle errors here
            }
            return RedirectToAction("ManageUsers");
        }
    }
}
