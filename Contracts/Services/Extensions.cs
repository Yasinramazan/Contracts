

using Contracts.Models;

namespace Contracts.Services
{
    public class Extensions
    {	//Static metodları iceren helperClass'ı
        public static int tryCatch(Action a)
        {
			int result = 0;
			try
			{
				a();
				result = 1;
			}
			catch (Exception e)
			{
				result = -1;
                throw new Exception(e.Message,e.InnerException);
            }
			return result;
        }
		public static double totalOperationPrice(List<double> prices,List<int> quantities,int length)
		{
			double result=0;
			for (int i = 0; i < length; i++)
			{
				result+=(prices[i] * quantities[i]) / 10;
			}
			return Math.Round(result,2) ;
		}
		public static double totalOperationCount(List<int> quantities)
		{
            double sum = quantities.Sum();
			return Math.Round((sum / 10),2) ;
        }
		public static double averagePrice(double totalOperationPrice,double totaloperationCount)
		{
			return Math.Round(totalOperationPrice / totaloperationCount, 2);
		}
		public static List<TableModel> sortListbyDateTime(List<TableModel> model)
		{
            List<TableModel> sortedList = model.OrderBy(t => t.Time).ToList();
			return sortedList;
        }
    }
}
