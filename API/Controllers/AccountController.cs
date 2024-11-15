using API.Dtos;
using API.Errors;
using API.Extentions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInUser;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInUser,ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInUser = signInUser;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
           // var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
           // var user = await _userManager.FindByEmailAsync(email);
           var user = await _userManager.FindByEmaiFromClaimsPrinciple(HttpContext.User);
             
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreatToken(user),
                DisplayName = user.DisplayName,
            };
        
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsasync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
       [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
         //   var email = HttpContext.User?.Claims?.
           //     FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByUserByClaimsPricipleWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto userAddress)
        {
            var user = await _userManager.FindByUserByClaimsPricipleWithAddressAsync(HttpContext.User);
            user.Address= _mapper.Map<AddressDto, Address>(userAddress);
            var result =await _userManager.UpdateAsync(user);
            if(result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest("Problem Upddaing request");
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
            if(CheckEmailExistsasync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { errors = new[] { "Email Address is in use" } });
            }
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
