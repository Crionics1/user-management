using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Helpers;
using UserManagement.Services;
using UserManagement.Web.ResponseTypes;

namespace UserManagement.Web.Controllers
{
    [Route("api/users/{privateId}/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private IAddressService _addressService;
        private IUserService _userService;
        public AddressesController(IAddressService addressService,IUserService userService)
        {
            _addressService = addressService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Address>> Get(string privateId)
        {
            BaseListResponse<Address> response = new BaseListResponse<Address>();
            try
            {
                response.Records = _addressService.GetByUserPrivateID(privateId);
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Address> Get(string privateId,int id)
        {
            try
            {
                var address = _addressService.Get(id);
                return address;
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

        [HttpPost]
        public ActionResult<Address> Post(string privateId,Address address)
        {
            try
            {
                address.UserID = _userService.GetByPrivateID(privateId).ID;
                _addressService.Create(address);
                return Created($"/api/users/{privateId}/addresses", address);
            }
            catch (Exception ex)
            {
                if (ex is BadRequestException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is NotFoundException)
                {
                    return BadRequest(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(string privateId,int id)
        {
            try
            {
                var adr = _addressService.Get(id);
                var user = _userService.GetByPrivateID(privateId);
                if (adr.UserID == user.ID)
                {
                    _addressService.Delete(id);
                    return Ok();
                }
                return BadRequest("Private ID is not associated with that address id!");
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

        [HttpPut("{id:int}")]
        public ActionResult Put(string privateId,int id,Address address)
        {
            try
            {
                address.ID = id;
                address.UserID = _userService.GetByPrivateID(privateId).ID;
                _addressService.Update(address);
                return Ok(address);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return BadRequest(ex.Message);
                }
                
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        }
    }
}