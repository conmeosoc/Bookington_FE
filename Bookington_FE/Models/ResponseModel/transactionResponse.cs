namespace Bookington_FE.Models.ResponseModel
{
    public class transactionResponse
    {
        public List<TransactionModel> result { get; set; } = new List<TransactionModel>();
        public PaginationU pagination { get; set; } = new PaginationU();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }
   
    public class TransactionModel
    {
        public string Id { get; set; } = null!;

        public string OrderId { get; set; } = null!;

        public string RefFrom { get; set; } = null!;

        public string FromUsername { get; set; } = null!;

        public string RefTo { get; set; } = null!;

        public string ToUsername { get; set; } = null!;

        public string Reason { get; set; } = null!;

        public DateTime CreateAt { get; set; }

        public double Amount { get; set; }
    }
}
