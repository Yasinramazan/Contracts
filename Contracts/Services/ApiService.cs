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

namespace Contracts.Services
{
    public class ApiService
    {
        String URLString = "https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history?endDate=2022-01-26&startDate=2022-01-26";
        public async Task<List<Trade>> getXMLFromURL (DateTime startDate,DateTime endDate)
        {
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
                throw (new Exception(response.RequestMessage.ToString()));
            }
        }


        public List<Trade> getTradeList(HttpResponseMessage response)
        {
            XDocument xdoc = XDocument.Parse(response.Content.ReadAsStringAsync().Result);

            StringReader sr = new StringReader(xdoc.ToString());

            DataSet ds = new DataSet();

            ds.ReadXml(sr);

            var body = ds.Tables[2];

            var dt = body.DefaultView.ToTable();
            List<Trade> tradesList = new List<Trade>();
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
            return tradesList;
        }
    }
}
