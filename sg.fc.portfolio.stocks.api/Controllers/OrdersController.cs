using Microsoft.AspNetCore.Mvc;
using Sg.Fc.Portfolio.Stocks.Api.Services;
using Sg.Fc.Portfolio.Stocks.Common.Domain;

namespace Sg.Fc.Portfolio.Stocks.Api.Controllers
{
    [ApiController]
    [Route("portfolio/[controller]")]
    public class OrdersController : ControllerBase
    {
        

        private readonly ILogger<StockQuoteController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(ILogger<StockQuoteController> logger, 
            IOrderService orderService)
        {
            _logger = logger;
           
            _orderService = orderService;
        }

      
        [HttpGet("all")]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
            
        }

        [HttpPost("add")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            bool result;
            try
            {
                 result = _orderService.AddOrder(order);
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);

        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            bool result;
            try
            {
                result = _orderService.DeleteOrder(id);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(result);

        }
    }
}