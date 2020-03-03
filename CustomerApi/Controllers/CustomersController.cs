using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CustomerApi.Data;
using CustomerApi.Models;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> repository;
        private readonly DataConverter converter;

        public CustomersController(IRepository<Customer> repos)
        {
            repository = repos;
            converter = new DataConverter();
        }

        // GET: Customers
        [HttpGet]
        public IEnumerable<CustomerDTO> Get()
        {
            var models = repository.GetAll();
            List<CustomerDTO> ret = new List<CustomerDTO>();
            foreach (var model in models)
            {
                ret.Add(converter.ModelToCustomerDTO(model));
            }

            return ret;
        }

        // GET: Customers/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var customer = repository.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(converter.ModelToCustomerDTO(customer));
        }

        // POST: Customers
        [HttpPost]
        public IActionResult Post([FromBody] CustomerDTO customer)                  //NEEDS TO BE IMPLEMENTED!!
        {
            if (customer == null)
            {
                return BadRequest("customer is null");
            }
            return NoContent();

        }

        // PUT: Customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CustomerDTO customer)                   //NEEDS TO BE IMPLEMENTED!!
        {
        }

        // DELETE: Customers/5
        [HttpDelete("{id}")]
        public void Delete(int id)                                                  //NEEDS TO BE IMPLEMENTED!!
        {
        }
    }
}
