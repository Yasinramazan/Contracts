using Contracts.Services;
using Contracts.Services.ContractService;
using Contracts.Services.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Contracts.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TradeController : Controller
    {
        private readonly IMediator _mediator;
        
        public TradeController(IMediator mediator)
        {
            _mediator = mediator;

        }
    
        
        [HttpGet] 
        public async Task<IActionResult> GetTradelistAsync()
        {   
            TradeResponse response = await _mediator.Send(new TradeRequest { StartDate=DateTime.Now,EndDate=DateTime.Now});

            
            return View(response);
        }
    }
}
