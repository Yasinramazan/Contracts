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
            //verileri API'den ceken ApiService sınıfına gidiyor
            var result = await _apiService.getXMLFromURLAsync(request.StartDate, request.EndDate);
            
            //API'den gelen verileri contract degerlerine gore filtreleyecek olan
            //filterListbyPH metoduna verileri gonderiyoruz ve keylerinin her biri
            //farklı contractlar iceren dictionary alıyoruz
            //Boylece veriler Contractlara gore ayrı listelere ayrısıyor
            tradeDictionary = _contractService.filterListbyPH(result);
            TradeResponse tradeResponse = new TradeResponse();
            tradeResponse.Table = new();

            foreach (var trade in tradeDictionary)
            {   //Dictionary'nin degerlerini donerek GetTableModel metodundan
                //son tabloda gosterilecek olan listeyi alıyor
                tradeResponse.Table.Add(_contractService.GetTableModel(trade.Value));
            }
            //Olusan tabloyu DateTime ozelligine gore sıralayan fonskiyon
            tradeResponse.Table = Extensions.sortListbyDateTime(tradeResponse.Table);
            
            return tradeResponse;
        }
    }
}
