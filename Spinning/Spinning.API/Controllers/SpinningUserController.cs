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
        private readonly UserManager<SpinningUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SpinningUserController(UserManager<SpinningUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Post : api/SpinningUser/Register
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
                await _userManager.AddToRoleAsync(user, "User");
                return user_DTO;
            }

            return BadRequest();
        }
        // Post: api/SpinningUser/Login

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(User_DTO user_DTO)
        {
            SpinningUser userFromDb = await _userManager.FindByEmailAsync(user_DTO.Email);
            if (userFromDb != null)
            {
                bool passwordValid = await _userManager.CheckPasswordAsync(userFromDb, user_DTO.Password);
                if (passwordValid)
                {
                    return Ok();
                }
            }

            return Unauthorized("Password invalid");
        }

        //Post : api/SpinningUser/Edit
        [HttpPost("Edit")]
        public async Task<ActionResult<User_DTO>> EditUser(User_DTO user_DTO)
        {
            SpinningUser userFromDb = await _userManager.FindByEmailAsync(user_DTO.Email);
            userFromDb.Email = user_DTO.Email;
            userFromDb.UserName = user_DTO.Email;
            userFromDb.FirstName = user_DTO.FirstName;
            userFromDb.LastName = user_DTO.LastName;

            IdentityResult result = await _userManager.UpdateAsync(userFromDb);
            if (result.Succeeded)
            {
                return Ok(user_DTO);
            }

            return BadRequest();
        }
    }
}