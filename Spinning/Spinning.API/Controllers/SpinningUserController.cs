using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spinning.API.DTOs;
using Spinning.Models;
using Spinning.Models.Data;
using Spinning.Models.Repositories;

namespace Spinning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpinningUserController : ControllerBase
    {
        private readonly SpinningDBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<SpinningUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SpinningUserController(SpinningDBContext context, IMapper mapper, UserManager<SpinningUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Post : api/SpinningUser
        [HttpPost("Register")]
        public async Task<ActionResult<User_DTO>> RegisterUser(User_DTO user_DTO)
        {
            var user = new SpinningUser
            {
                Email = user_DTO.Email,
                UserName = user_DTO.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddPasswordAsync(user, user_DTO.Password);
                return user_DTO;
            }

            return BadRequest();
        }
        // Post: api/SpinningUser

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(User_DTO user_DTO)
        {
            SpinningUser userFromDB = await _userManager.FindByEmailAsync(user_DTO.Email);
            if (userFromDB != null)
            {
                var passwordValid = await _userManager.CheckPasswordAsync(userFromDB, user_DTO.Password);
                if (passwordValid)
                {
                    return Ok();
                }
            }
            return Unauthorized("Password invalid");
        
        }
    }
}