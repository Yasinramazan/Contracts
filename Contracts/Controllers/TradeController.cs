using Contracts.Services;
using Contracts.Services.ContractService;
using Contracts.Services.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IMediator _mediator;
        
        public TradeController(IMediator mediator)
        {
            _mediator = mediator;
           
        }
        
        [HttpGet] 
        public async Task<IActionResult> GetTradelist()
        {
            TradeResponse response = await _mediator.Send(new TradeRequest { StartDate=DateTime.Now,EndDate=DateTime.Now});

            
            return Ok(response);
        }
    }
}
