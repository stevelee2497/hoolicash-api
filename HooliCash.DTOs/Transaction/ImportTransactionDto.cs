namespace HooliCash.DTOs.Transaction
{
    public class ImportTransactionDto
    {
        public int ID { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Wallet { get; set; }
        public string Currency { get; set; }
        public string Date { get; set; }
        public string ExcludeReport { get; set; }
    }
}
