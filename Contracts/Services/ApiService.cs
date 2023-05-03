using Contracts.Models;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Contracts.Services.Exceptions;

namespace Contracts.Services
{
    public class ApiService
    {   //Veriyi API'den ceken class
        

        
        public async Task<List<Trade>> getXMLFromURLAsync (DateTime startDate,DateTime endDate)
        {
            var dateString1 = startDate.ToString("yyyy-MM-dd");
            var dateString2 = endDate.ToString("yyyy-MM-dd");//Gelen DateTime degiskenlerini uygun formata cevirir

            //URL'yi AppSettings.json'dan almayı düsündüm ancak parametre alacagı icin vazgectim
            String URLString = $"https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate={dateString2}&startDate={dateString1}";

            List<Trade> trades = new List<Trade>();
            
            var client = new HttpClient();
            
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                HttpResponseMessage response = client.GetAsync(URLString).Result;
            
               

            if (response.StatusCode == HttpStatusCode.OK)
            {
            return getTradeList(response);
            }
            else
            {
                throw new URLNotFoundException(response.Content.ToString(),new Exception(response.StatusCode.ToString()));
            }
        }


        public List<Trade> getTradeList(HttpResponseMessage response)
        {
            List<Trade> tradesList = new List<Trade>();
            Extensions.tryCatch(() =>
            {
                XDocument xdoc = XDocument.Parse(response.Content.ReadAsStringAsync().Result);

                StringReader sr = new StringReader(xdoc.ToString());

                DataSet ds = new DataSet();

                ds.ReadXml(sr);//Okunan veriyi dataTable'a cevirir

                var body = ds.Tables[2];//body node secer

                var dt = body.DefaultView.ToTable();//body node degerlerini table'a cevirir
                
                //Table'ın her verisini donerek bir Trade listesi olusturur
                foreach (DataRow row in dt.Rows)
                {   
                    var values = row.ItemArray;
                    Trade trade = new Trade()
                    {
                        Id = values[0].ToString(),
                        Date = Convert.ToDateTime(values[1]),
                        Contract = values[2].ToString(),
                        Price = Double.Parse(values[3].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                        Quantity = Convert.ToInt32(values[4])
                    };
                    tradesList.Add(trade);
                }
            });
            
            
            return tradesList;
        }
    }
}
