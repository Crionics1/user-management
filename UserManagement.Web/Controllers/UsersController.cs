using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Helpers;
using UserManagement.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<User[]> Get()
        {
            try
            {
                return Ok(_userService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpGet("{privateId}")]
        public ActionResult<User> Get(string privateId)
        {
            try
            {
                return Ok(_userService.GetByPrivateID(privateId));
            }
            catch (Exception ex)
            {
                if (ex  is NotFoundException)
                {
                   return NotFound(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("mail/{email}")]
        public ActionResult<User> GetByEmail(string email)
        {
            try
            {
                return Ok(_userService.GetByEmail(email));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
