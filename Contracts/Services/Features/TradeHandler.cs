using MediatR;

namespace Contracts.Services.Features
{
    public class TradeHandler : IRequestHandler<TradeRequest, TradeResponse>
    {
        private readonly ApiService _apiService;
        private readonly ContractService.ContractService _contractService;
        public TradeHandler(ApiService apiService, ContractService.ContractService contractService)
        {
            _apiService = apiService;
            _contractService = contractService;
        }
        public async Task<TradeResponse> Handle(TradeRequest request, CancellationToken cancellationToken)
        {
            TradeUseCase tradeUseCase = new TradeUseCase(_apiService,_contractService);
            TradeResponse result=await tradeUseCase.GetTradeList(request);
            return result;
        }
    }
}
