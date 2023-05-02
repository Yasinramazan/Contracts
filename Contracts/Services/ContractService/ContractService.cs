using Contracts.Models;
using System.Linq;

namespace Contracts.Services.ContractService
{
    public class ContractService
    {
        
        public ContractService()
        {
            
        }

        public Dictionary<string, List<Trade>> filterListbyPH(List<Trade> tradeList) 
        {
            
            List<Trade> filteredList= tradeList.Where(x=>x.Contract.Substring(0,2)=="PH").ToList();
            
           
            Dictionary<string, List<Trade>> tradeDictionary = new Dictionary<string, List<Trade>>();
            foreach (var item in filteredList)
            {
                if (!tradeDictionary.ContainsKey(item.Contract))
                {
                    tradeDictionary.Add(item.Contract, new List<Trade>());
                }
                
                tradeDictionary.FirstOrDefault(x => x.Key == item.Contract).Value.Add(item);
            }
            
            return tradeDictionary;
        }

        public TableModel GetTableModel(List<Trade> tradeList)
        {
            
            int year = int.Parse("20"+tradeList[0].Contract.Substring(2, 2)) ;
            int month = int.Parse(tradeList[0].Contract.Substring(4, 2));
            int day = int.Parse(tradeList[0].Contract.Substring(6, 2));
            int time = int.Parse(tradeList[0].Contract.Substring(8, 2));
            TableModel tableModel = new TableModel();
            tableModel.Time.ToString($"{month}/{day}/{year} {time}:00");
            tableModel.Time = new DateTime(year, month, day, time,0,0);
            List<double> prices = tradeList.Select(x => x.Price).ToList();
            List<int> quantities = tradeList.Select(x => x.Quantity).ToList();
            
            tableModel.TotalOperationPrice = Extensions.totalOperationPrice(prices,quantities,tradeList.Count);
            tableModel.TotalOperationCount=Extensions.totaloperationCount(quantities);
            tableModel.AveragePrice = Extensions.averagePrice(tableModel.TotalOperationPrice, tableModel.TotalOperationCount);

            return tableModel;
        }
    }
}
