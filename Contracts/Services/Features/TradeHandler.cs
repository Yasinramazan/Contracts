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
            TradeResponse result=await tradeUseCase.GetTradeList(request);// Bu sınıftan UseCase sınıfına ve
            return result;                                                // oradan da verileri cekecek olan Apiservice'e gidiyor
                                                                          // Oradan da Contract sınıfına giden veriler tablo degerleri olarak 
                                                                          //buraya donuyor
        }
    }
}
