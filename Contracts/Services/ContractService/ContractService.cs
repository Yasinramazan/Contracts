using Contracts.Models;
using System.Linq;

namespace Contracts.Services.ContractService
{
    public class ContractService
    {   //Contracta dair islemleri yapan class
        //Tabloya yansıyacak olan son degerler burada hazır oluyor
        
        public ContractService()
        {
            
        }

        public Dictionary<string, List<Trade>> filterListbyPH(List<Trade> tradeList) 
        {   //Verileri Contractlara gore filtreleyen metod
            
            List<Trade> filteredList= tradeList.Where(x=>x.Contract.Substring(0,2)=="PH").ToList();// Contract ozelligi PH olan
                                                                                                   //verileri filtreleyen fonksiyon
           
            Dictionary<string, List<Trade>> tradeDictionary = new Dictionary<string, List<Trade>>();
            foreach (var item in filteredList)
            {
                if (!tradeDictionary.ContainsKey(item.Contract))
                {
                    tradeDictionary.Add(item.Contract, new List<Trade>());//Contract ozelligini key olarak kullanan dictionary olusturur
                }
                
                tradeDictionary.FirstOrDefault(x => x.Key == item.Contract).Value.Add(item);//O contract Key'ine ait olan verileri de dictionary value'su olarak ekler
            }
            
            return tradeDictionary;
        }

        public TableModel GetTableModel(List<Trade> tradeList)
        {   //Tabloda gosterilecek son degerlerin TableModel uzerinde hazırlanmasını yapan metod
            //Parametre olarak her farklı contract degerine ait model listesini alıyor
            
            int year = int.Parse("20"+tradeList[0].Contract.Substring(2, 2)) ;
            int month = int.Parse(tradeList[0].Contract.Substring(4, 2));
            int day = int.Parse(tradeList[0].Contract.Substring(6, 2));
            int time = int.Parse(tradeList[0].Contract.Substring(8, 2));//Substring fonksiyonuyla contract ozelligindeki datetime'ı ayrıstıyor
            
            TableModel tableModel = new TableModel();//Tablonun her bir satırına denk gelen TableModel nesnesi icin yeni bir model uretir
            
            tableModel.Time = new DateTime(year, month, day, time,0,0);
            
            List<double> prices = tradeList.Select(x => x.Price).ToList();
            List<int> quantities = tradeList.Select(x => x.Quantity).ToList();
            
            //Islemlerin kod kalabalıgından kacınmak ve fonksiyonları gerekirse
            //tekrar kullanabilmek adina static metodlar uzerinden
            //sonucları alan kısım
            tableModel.TotalOperationPrice = Extensions.totalOperationPrice(prices,quantities,tradeList.Count);
            tableModel.TotalOperationCount=Extensions.totalOperationCount(quantities);
            tableModel.AveragePrice = Extensions.averagePrice(tableModel.TotalOperationPrice, tableModel.TotalOperationCount);

            return tableModel;
        }
    }
}
