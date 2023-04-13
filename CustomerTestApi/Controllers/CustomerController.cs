using CustomerTestApi.Model;
using CustomerTestApi.Models;
using CustomerTestApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CustomerTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        protected CustomerResponse _response;
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
            this._response = new CustomerResponse();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task <ActionResult<CustomerResponse>>Get(int id)
        {
            try
            {
                var customers = await _customerService.GetCustomerByIdAsync(id);

                if (customers.Count == 0)
                {
                    _response.DisplayMessage = "Customer does not exist!";
                    return NotFound(_response);
                }
                _response.Result = customers;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult<CustomerResponse>> Post(Customer customerDto )
        {
            try
            {
                var customer = await _customerService.CreateCustomerAsync(customerDto);
                _response.Result = customer;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }
    }
}
