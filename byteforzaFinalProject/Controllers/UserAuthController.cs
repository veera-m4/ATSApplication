using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace byteforzaFinalProject.Controllers
{
	public class UserAuthController : Controller
	{
        private readonly IUserInterface _authService;
        public UserAuthController(IUserInterface userService)
        {
            this._authService = userService;
        }
		public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            if (result.result == "success")
            {
				Console.WriteLine(User.IsInRole("staff"));
          
				Console.WriteLine(User.IsInRole("staff"));
				Console.WriteLine(User.IsInRole("staff"));
				if (User.IsInRole("staff"))
				{
					return RedirectToAction("HomePage", "ATSApplication");
				}
				return RedirectToAction("Index", "CandidateApplication");
			}
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(CandidateRegisterDTO model)
        {
            if (!ModelState.IsValid) { return View(model); }
            var result = await this._authService.RegisterAsync(model);
            if(result.result != "success")
            {
				TempData["msg"] = result.Message;
				return RedirectToAction(nameof(Registration));
			}
            Console.WriteLine(User.IsInRole("staff"));
			Console.WriteLine(User.IsInRole("staff"));
			Console.WriteLine(User.IsInRole("staff"));
			if (User.IsInRole("staff"))
            {
				return RedirectToAction("HomePage", "ATSApplication");
			}
			return RedirectToAction("Index", "CandidateApplication");
		}
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
