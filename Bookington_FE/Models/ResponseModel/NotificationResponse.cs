namespace Bookington_FE.Models.ResponseModel
{
	public class NotificationResponse
	{
		public List<NotificationModel> result { get; set; } = new List<NotificationModel>();
		public int statusCode { get; set; }
		public bool isError { get; set; } = false;
		public string message { get; set; } = string.Empty;
	}
	public class NotificationModel
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string RefAccount { get; set; } = null!;

		public string Content { get; set; } = null!;

		public DateTime CreateAt { get; set; } = DateTime.Now;

		public bool IsRead { get; set; } = false;
	}
}
