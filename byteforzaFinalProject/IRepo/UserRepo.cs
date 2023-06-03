﻿using byteforzaFinalProject.DatabaseContext;
using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Interface;
using byteforzaFinalProject.Models;
using byteforzaFinalProject.Response;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace byteforzaFinalProject.IRepo
{
	public class UserRepo : IUserInterface
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private ApplicationDbContext _db;
		public UserRepo(ApplicationDbContext db, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.signInManager = signInManager;
			this._db = db;

		}

		public async Task<FormResponse> LoginAsync(LoginDTO model)
		{
			var status = new FormResponse();
			var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
			{
				status.result = "failed";
				status.Message = "Invalid email";
				return status;
			}

			if (!await userManager.CheckPasswordAsync(user, model.Password))
			{
				status.result = "failed";
				status.Message = "Invalid Password";
				return status;
			}

			var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
			if (signInResult.Succeeded)
			{
				var userRoles = await userManager.GetRolesAsync(user);
				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Email, user.Email),
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}
				status.result = "success";
				status.Message = "Logged in successfully";
			}
			else if (signInResult.IsLockedOut)
			{
				status.result = "failed";
				status.Message = "User is locked out";
			}
			else
			{
				status.result = "failed";
				status.Message = "Error on logging in";
			}

			return status;
		}
		public async Task LogoutAsync()
		{
			await signInManager.SignOutAsync();
		}

		public async Task<FormResponse> RegisterAsync(CandidateRegisterDTO model)
		{
			var  status = new FormResponse();
			var userExists = await userManager.FindByNameAsync(model.Email);
			if (userExists != null)
			{
				status.result = "failed";
				status.Message = "Email already exist";
				return status;
			}
			ApplicationUser user = new ApplicationUser()
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FirstName = model.FirstName,
				LastName = model.LastName,
				EmailConfirmed = true,
			};
			var result = await userManager.CreateAsync(user, model.password);
			if (!result.Succeeded)
			{
				status.result = "failed";
				status.Message = "User creation failed";
				return status;
			}

			if (!await roleManager.RoleExistsAsync(model.Role))
				await roleManager.CreateAsync(new IdentityRole(model.Role));


			if (await roleManager.RoleExistsAsync(model.Role))
			{
				await userManager.AddToRoleAsync(user, model.Role);
			}
			Thread.Sleep(5000);
            var user1 = await userManager.FindByNameAsync(model.Email);
            var signInResult = await signInManager.PasswordSignInAsync(user1, model.password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user1);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user1.Email),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.result = "success";
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.result = "failed";
                status.Message = "User is locked out";
            }
            else
            {
                status.result = "failed";
                status.Message = "Error on logging in";
            }
            status.result = "success";
			return status;
		}

		
	}
}