namespace Bookington_FE.Models.ResponseModel
{
    public class DashboardAdmin
    {
        public DashboardAModel result { get; set; } = new DashboardAModel();
		public List<NotificationModel> noti { get; set; } = new List<NotificationModel>();

		public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }


    public class DashboardAModel
    {
        public int TotalOrders { get; set; } = 0;

		public int PaidOrders { get; set; } = 0;


		public int CanceledOrders { get; set; } = 0;


		public int RefundedOrders { get; set; } = 0;

		public double TotalSales { get; set; } = 0;


		public double AverageSale { get; set; } = 0;

		public double CommissionEarned { get; set; } = 0;

	}
}
