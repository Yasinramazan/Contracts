

namespace Contracts.Services
{
    public class Extensions
    {
        public static int tryCatch(Action a)
        {
			int result = 0;
			try
			{
				a();
				result = 1;
			}
			catch (Exception)
			{

				result = -1;
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
		public static double totaloperationCount(List<int> quantities)
		{
            double sum = quantities.Sum();
			return Math.Round((sum / 10),2) ;
        }
		public static double averagePrice(double totalOperationPrice,double totaloperationCount)
		{
			return Math.Round(totalOperationPrice / totaloperationCount, 2);
		}
    }
}
