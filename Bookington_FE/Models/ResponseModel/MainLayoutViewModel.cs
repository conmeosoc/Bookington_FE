namespace Bookington_FE.Models.ResponseModel
{
    public class MainLayoutViewModel
    {
        public Pagination pagination { get; set; } = new Pagination();
        public List<NotificationModel> ArrayNotification { get; set; } = new List<NotificationModel>();
        public MainLayoutViewModel() { }
    }
}
