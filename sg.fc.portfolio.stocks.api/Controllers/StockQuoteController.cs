using Microsoft.AspNetCore.Mvc;
using Sg.Fc.Portfolio.Stocks.Api.Services;
using Sg.Fc.Portfolio.Stocks.Common.Domain;

namespace Sg.Fc.Portfolio.Stocks.Api.Controllers
{
    [ApiController]
    [Route("portfolio/[controller]")]
    public class StockQuoteController : ControllerBase
    {
        

        private readonly ILogger<StockQuoteController> _logger;
        private readonly PortfolioService _portfolioService;
       

        public StockQuoteController(ILogger<StockQuoteController> logger, 
            PortfolioService portfolioService
            )
        {
            _logger = logger;
            _portfolioService = portfolioService;
          
        }

        [HttpGet("symbolsearch")]
        public async Task<IActionResult> SymbolSearch([FromQuery]string keywords)
        {
            return Ok(await _portfolioService.GetStockSymbols(keywords));
        }

        [HttpGet("ltp")]
        public async Task<IActionResult> GetLTP([FromQuery] string symbol,[FromQuery] string date)
        {
            var ohlc = await _portfolioService.GetLTP(symbol, date);
            if(ohlc == null)
                return NotFound();
            return Ok(ohlc);
        }

       
    }
}