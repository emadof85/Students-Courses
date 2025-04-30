using Application.DTOs;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Students_Courses.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Admin", Text = "Admin" },
                    new SelectListItem { Value = "Teacher", Text = "Teacher" },
                    new SelectListItem { Value = "Student", Text = "Student" }
                }
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // إعادة ملء الـ Roles في حال وجود أخطاء وإعادة عرض النموذج
                model.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Teacher", Text = "Teacher" },
                new SelectListItem { Value = "Student", Text = "Student" }
            };
                return View(model);
            }

            // تحويل بيانات الـ RegisterViewModel إلى RegisterDto (أو كائن آخر يستخدم في AuthService)
            var dto = new RegisterDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Role == "Student" ? model.Age.GetValueOrDefault() : 0, // إذا كان للطالب فقط
                Email = model.Email,
                Address = model.Address,
                Password = model.Password,
                Role = model.Role
            };

            var result = await _authService.RegisterAsync(dto);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            model.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Teacher", Text = "Teacher" },
                new SelectListItem { Value = "Student", Text = "Student" }
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(dto);

            var result = await _authService.LoginAsync(dto);
            if (result.Succeeded)
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }

}
