namespace Contracts.Models
{
    public class TableModel
    {
        public DateTime Time { get; set; }
        public double TotalOperationCount { get; set; }
        public double TotalOperationPrice { get; set; }
        public double  AveragePrice { get; set; }
    }
}
