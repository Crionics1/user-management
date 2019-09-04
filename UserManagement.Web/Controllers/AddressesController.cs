using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
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
                _addressService.Create(address);
                return Created($"/api/users/{privateId}/addresses", address);
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

        [HttpDelete("{id:int}")]
        public ActionResult Delete(string privateId,int id)
        {
            try
            {
                _addressService.GetByUserPrivateID(privateId);
                _addressService.Delete(id);
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