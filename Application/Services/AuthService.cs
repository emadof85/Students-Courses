using Application.DTOs;
using Application.IServices;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStudentRepository _studentRepository;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentRepository = studentRepository;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            ApplicationUser user;
                        
            if (dto.Role == "Student")
            {
                
                user = new Student
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    Name = $"{dto.FirstName} {dto.LastName}",
                    Address = dto.Address,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    IsActive = true
                };
            }
            else
            {
                user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    Name = $"{dto.FirstName} {dto.LastName}",
                    Address = dto.Address
                };
                
            }
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }
            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDto dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}
