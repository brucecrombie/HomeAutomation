using System;

namespace Csv2QifConverter.Lib
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Account { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public String Currency { get; set; }
        public decimal Commission { get; set; }
        public decimal Price { get; set; }
        public decimal NetAmount { get; set; }
        public string AccountNickname { get; set; }
        public string Status { get; set; }
        public string QIFDate { get; set; }
        public string QIFAction { get; set; }
        public string QIFSecurity { get; set; }
        public string QIFCleared { get; set; }
        public string QIFQuantity { get; set; }
        public string QIFPrice { get; set; }
        public string QIFUAmount { get; set; }
        public string QIFTAmount { get; set; }
        public string QIFCategory { get; set; }
        public string QIFDelimiter { get; set; }
    }
}