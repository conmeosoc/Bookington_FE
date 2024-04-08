namespace Bookington_FE.Models.ResponseModel
{
    public class DashboardOwnerResponse
    {
        public DashboardOwnerModel result { get; set; } = new DashboardOwnerModel();
		public List<NotificationModel> noti { get; set; }= new List<NotificationModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;

    }
    public class DashboardOwnerModel
    {
        public int TotalOrders { get; set; } = 0;
        public int RefundedOrders { get; set; } = 0;
		public int ApprovedOrders { get; set; } = 0;
		public int RejectedOrders { get; set; } = 0;
		public int TotalBookings { get; set; } = 0;
		public double TotalIncomes { get; set; } = 0;
		public double CommissionPaid { get; set; } = 0;
		public double TotalEarnings { get; set; } = 0;
		public double AverageEarnings { get; set; } = 0;
		public double TotalRevenue { get; set; } = 0;
		public double AverageRevenue { get; set; } = 0;
		public double RefundRevenue { get; set; } = 0;
	}
}
