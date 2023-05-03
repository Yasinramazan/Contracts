namespace Contracts.Models
{
    public class TableModel
    {   // En son tabloda gosterilecek verilerin modeli
        public DateTime Time { get; set; }
        public double TotalOperationCount { get; set; }
        public double TotalOperationPrice { get; set; }
        public double  AveragePrice { get; set; }
    }
}
