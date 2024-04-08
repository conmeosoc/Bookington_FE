namespace Bookington_FE.Models.ResponseModel
{
    public class SlotResponse
    {
        public List<SlotModel> result { get; set; } = new List<SlotModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }
    

    public class SlotModel
    {
        public string Id { get; set; }

        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string daysInSchedule { get; set; }
        public double price { get; set; }
        public bool IsActive { get; set; }
    
    }
}
