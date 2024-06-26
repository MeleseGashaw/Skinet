﻿using API.Dtos;
using API.Errors;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInUser;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInUser,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInUser = signInUser;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>LogIn(LogInDto logInDto)
        {
            var user= await _userManager.FindByEmailAsync(logInDto.Email);
            if (user == null) { return Unauthorized(new ApiResponse(401)); }
            var result = await _signInUser.CheckPasswordSignInAsync(user, logInDto.Password, false);
            if (!result.Succeeded) { return Unauthorized(new ApiResponse(401)); }
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreatToken(user),
                DisplayName = user.DisplayName,
            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Registor(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.DisplayName,

            };
            var result = await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) { return BadRequest(new ApiResponse(400)); }
                return new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreatToken(user),
                    DisplayName = user.DisplayName,
                };
            
        }
      
}
}
