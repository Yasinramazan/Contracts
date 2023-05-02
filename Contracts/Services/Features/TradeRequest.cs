using MediatR;

namespace Contracts.Services.Features
{
    public class TradeRequest:IRequest<TradeResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
