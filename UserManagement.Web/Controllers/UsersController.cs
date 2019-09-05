using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Helpers;
using UserManagement.Services;
using System.Collections;
using System.Collections.Generic;
using UserManagement.Web.ResponseTypes;
using System.Data;

namespace UserManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IAddressService _addressService;

        public UsersController(IUserService userService, IAddressService addressService)
        {
            _userService = userService;
            _addressService = addressService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            BaseListResponse<User> response = new BaseListResponse<User>();

            try
            {
                response.Records = _userService.GetAll();
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{privateId}")]
        public ActionResult<User> Get(string privateId)
        {
            try
            {
                var user = _userService.GetByPrivateID(privateId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("GetByEmail/{email}")]
        public ActionResult<User> GetByEmail(string email)
        {
            try
            {
                var user = _userService.GetByEmail(email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {

                    return NotFound(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("GetByMobile/{mobile}")]
        public ActionResult<User> GetByMobile(string mobile)
        {
            try
            {
                var user = _userService.GetByMobile(mobile);
                return Ok(user);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {

                    return NotFound(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("GetByFirstName/{firstName}")]
        public ActionResult<IEnumerable<User>> GetByFirstNAme(string firstName)
        {
            BaseListResponse<User> response = new BaseListResponse<User>();

            try
            {
                response.Records = _userService.GetByFirstName(firstName);
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("GetByLastName/{lastName}")]
        public ActionResult<IEnumerable<User>> GetByLastNAme(string lastName)
        {
            BaseListResponse<User> response = new BaseListResponse<User>();

            try
            {
                response.Records = _userService.GetByLastName(lastName);
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            try
            {
                _userService.Create(user);
                return Created($"/api/users/{user.PrivateID}", user);
            }
            catch (Exception ex)
            {
                if (ex is BadRequestException)
                {
                    return BadRequest(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put(int id,User user)
        {
            try
            {
                user.ID = id;
                _userService.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                if (ex is BadRequestException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{userId}")]
        public ActionResult Delete(int userId)
        {
            try
            {
                _userService.Delete(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return BadRequest(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
