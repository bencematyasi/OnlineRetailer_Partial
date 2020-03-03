using System;
using System.Collections.Generic;
using System.Linq;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Models;
using RestSharp;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> repository;
        private readonly DataConverter converter;

        public OrdersController(IRepository<Order> repos)
        {
            repository = repos;
            converter = new DataConverter();
        }

        // GET: orders
        [HttpGet]
        public IEnumerable<OrderDTO> Get()
        {
            var models = repository.GetAll();
            List<OrderDTO> ret = new List<OrderDTO>();
            foreach (var model in models)
            {
                ret.Add(converter.ModelToOrderDTO(model));
            }

            return ret;
        }

        // GET orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(converter.ModelToOrderDTO(item));
        }

        // POST orders
        [HttpPost]
        public IActionResult Post([FromBody]OrderDTO order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            if (!CheckCustomerExists(order.CustomerId))
            {
                return BadRequest("Customer cannot be found");
            }

            // Call ProductApi to get the product ordered
            RestClient c = new RestClient();
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            c.BaseUrl = new Uri("https://localhost:5001/products/");
            if (order.OrderLines.Any())
            {
                foreach (var orderline in order.OrderLines)
                {
                    var request = new RestRequest(orderline.ProductId.ToString(), Method.GET);
                    var response = c.Execute<ProductDTO>(request);
                    var orderedProduct = response.Data;

                    if (orderline.Quantity <= orderedProduct.ItemsInStock - orderedProduct.ItemsReserved)
                    {
                        // reduce the number of items in stock for the ordered product,
                        // and create a new order.
                        orderedProduct.ItemsReserved += orderline.Quantity;
                        var updateRequest = new RestRequest(orderedProduct.Id.ToString(), Method.PUT);
                        updateRequest.AddJsonBody(orderedProduct);
                        var updateResponse = c.Execute(updateRequest);

                        if (updateResponse.IsSuccessful)
                        {
                            var newOrder = repository.Add(converter.OrderDTOToModel(order));
                            return CreatedAtRoute("GetOrder", new { id = newOrder.Id }, converter.ModelToOrderDTO(newOrder));
                        }
                    }
                }
            }

            // If the order could not be created, "return no content".
            return NoContent();
        }

        //WIP, needs refinements
        private bool CheckCustomerExists(int id)
        {
            RestClient c = new RestClient();

            c.BaseUrl = new Uri("https://localhost:5001/customers/");
            var request = new RestRequest(id.ToString(), Method.GET);
            var response = c.Execute<CustomerDTO>(request);

            //this part
            if (response.Data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
