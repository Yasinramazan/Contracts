using Contracts.Models;

namespace Contracts.Services.Features
{
    public class TradeUseCase
    {
        private readonly ApiService _apiService;
        private readonly ContractService.ContractService _contractService;
        public TradeUseCase(ApiService apiService, ContractService.ContractService contractService)
        {
            _apiService = apiService;
            _contractService = contractService;
        }

        public async Task<TradeResponse> GetTradeList(TradeRequest request)
        {
            Dictionary<string,List<Trade>> tradeDictionary= new();
            var result = await _apiService.getXMLFromURL(request.StartDate, request.EndDate);
            
            
            tradeDictionary = _contractService.filterListbyPH(result);
            TradeResponse tradeResponse = new TradeResponse();
            tradeResponse.Table = new();
            foreach (var trade in tradeDictionary)
            {
                tradeResponse.Table.Add(_contractService.GetTableModel(trade.Value));
            }
            
            
            return tradeResponse;
        }
    }
}
