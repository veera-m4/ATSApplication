using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Interface;
using byteforzaFinalProject.Response;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if(User.IsAuthenticated())
            {
                FormResponse formResponse = new FormResponse();
                formResponse.result = "success";
				return RedirectToAction("RedirectAction", formResponse);
			}
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            return RedirectToAction("RedirectAction",result );
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
            return RedirectToAction("RedirectAction", result);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult RedirectAction(FormResponse result1)
        {
            
            if (result1.result == "success")
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
                TempData["msg"] = result1.Message;
                return RedirectToAction(nameof(Login));
            }
        }
    }
}